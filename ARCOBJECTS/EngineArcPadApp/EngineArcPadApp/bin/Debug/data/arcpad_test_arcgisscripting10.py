import arcgisscripting
arcpy = arcgisscripting.create(9.3)
arcpy.AddToolbox(r'C:\Program Files (x86)\ArcGIS\ArcPad10.2\DesktopTools10.0\Toolboxes\ArcPad Tools.tbx')
try:
   arcpy.ArcPadCheckout_Arcpad(r'C:\Uzumaki\Incidents\1281269_TrimbleArcPad\EngineArcPadApp\EngineArcPadApp\bin\Debug\data\Riverside.gdb\Parcel;C:\Uzumaki\Incidents\1281269_TrimbleArcPad\EngineArcPadApp\EngineArcPadApp\bin\Debug\data\Riverside.gdb\Streets', '#', '#', '#', r'C:\Uzumaki\Incidents\1281269_TrimbleArcPad\EngineArcPadApp\EngineArcPadApp\bin\Debug\data\Riverside.axf')
   print(arcpy.GetMessages(0))
except arcpy.ExecuteError: print(arcpy.GetMessages(2))
except: print('Generic Exception')
finally: arcpy.RemoveToolbox(r'C:\Program Files (x86)\ArcGIS\ArcPad10.2\DesktopTools10.0\Toolboxes\ArcPad Tools.tbx')
