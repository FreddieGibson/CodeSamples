using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.Geodatabase;
using System;
using System.Collections;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;

namespace ArcMapAddinUniqueValueRenderer
{
    public class UniqueValueRendererUserData : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        private static readonly string SRC = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "data");
        private static readonly string LPK = Path.Combine(SRC, "UserData.lpk");

        private const string TableName = "RoadMainScores";
        private const string JoinField = "RC1";

        protected override void OnClick()
        {
            // Clear all layers from the TOC
            ArcMap.Document.FocusMap.ClearLayers();

            // Open the data included with the sample
            IFeatureLayer packageLayer = OpenLayerPackage(LPK);
            packageLayer.Name = "Initial Layer";

            // Add the original layer to the map document
            ArcMap.Document.FocusMap.AddLayer(packageLayer);

            IFeatureLayer updatedLayer = OpenLayerPackage(LPK);
            updatedLayer.Name = "Updated Layer";

            // Add the updated layer to the map
            ArcMap.Document.FocusMap.AddLayer(updatedLayer);

            // Refresh the display
            ArcMap.Document.ActiveView.Refresh();
            ArcMap.Document.ActiveView.ContentsChanged();

            // Generate the unique value renderer using DataStatistics
            IUniqueValueRenderer uvRenderer = GenerateUniqueValueRenderer(packageLayer);

            // Update the layers symbology
            ((IGeoFeatureLayer)ArcMap.Document.FocusMap.Layer[0]).Renderer = (IFeatureRenderer)uvRenderer;

            // Force refresh of the TOC
            ArcMap.Document.ActiveView.ContentsChanged();
        }

        private static IUniqueValueRenderer GenerateUniqueValueRenderer(IFeatureLayer featureLayer)
        {
            IFeatureWorkspace featureWorkspace = ((IDataset) featureLayer.FeatureClass).Workspace as IFeatureWorkspace;
            ITable table = featureWorkspace.OpenTable(TableName);

            string relClassName = string.Format("{0}_{1}", ((IDataset) featureLayer.FeatureClass).Name, ((IDataset) table).Name);
            IRelationshipClass relClass = featureWorkspace.OpenRelationshipClass(relClassName);

            IDisplayRelationshipClass displayRelClass = featureLayer as IDisplayRelationshipClass;
            if (displayRelClass == null) return null;
            displayRelClass.DisplayRelationshipClass(relClass, esriJoinType.esriLeftOuterJoin);

            IUniqueValueRenderer uvRenderer = new UniqueValueRendererClass { FieldCount = 1 };
            uvRenderer.Field[0] = string.Format("{0}.{1}", ((IDataset)table).Name, JoinField);

            IFeatureCursor cursor = ((IGeoFeatureLayer) featureLayer).SearchDisplayFeatures(null, true);
            DataStatisticsClass dataStatistics = new DataStatisticsClass
            {
                Field = uvRenderer.Field[0],
                Cursor = cursor as ICursor
            };

            IEnumerator pEnumerator = dataStatistics.UniqueValues;
            pEnumerator.Reset();

            Random random = new Random();
            while (pEnumerator.MoveNext())
            {
                string value = Convert.ToString(pEnumerator.Current);

                RgbColorClass fillColor = new RgbColorClass
                {
                    Red = random.Next(0, 255),
                    Green = random.Next(0, 255),
                    Blue = random.Next(0, 255)
                };

                ISimpleLineSymbol fillSymbol = new SimpleLineSymbolClass { Color = fillColor, Width = 2};
                uvRenderer.AddValue(value, "ZONE", fillSymbol as ISymbol);
            }

            Marshal.ReleaseComObject(cursor);
            return uvRenderer;
        }

        private static IFeatureLayer OpenLayerPackage(string pathToLPK)
        {
            ILayerFile layerFile = new LayerFileClass();
            layerFile.Open(pathToLPK);
            return layerFile.Layer as IFeatureLayer;
        }

        protected override void OnUpdate() { }
    }
}
