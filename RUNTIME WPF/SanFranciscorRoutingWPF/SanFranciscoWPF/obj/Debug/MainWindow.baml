   M S B A M L   `   `   `  ����  SanFranciscoWPF%    SanFranciscoWPF.RoutingBarriers   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      ` \ESRI.ArcGIS.Client.Local, Version=10.2.3.0, Culture=neutral, PublicKeyToken=29c6dd6e8553d944Z VESRI.ArcGIS.Client, Version=10.2.3.0, Culture=neutral, PublicKeyToken=29c6dd6e8553d9447esri*http://schemas.esri.com/arcgis/client/2009   5      .��5      � -   5          �Name$  
LayoutRoot��5      
������� � 	Resources 5   
      CompositeSymbol �� Path3  -ESRI.ArcGIS.Client.Symbols.SimpleMarkerSymbol  StopsSymbol   Size   Style   Color'  !ESRI.ArcGIS.Client.Symbols.Symbol   ControlTemplate1  +ESRI.ArcGIS.Client.Symbols.SimpleFillSymbol  BlockSymbol+  %ESRI.ArcGIS.Client.Symbols.FillSymbol   Fill   BorderBrush	   BorderThickness1  +ESRI.ArcGIS.Client.Symbols.SimpleLineSymbol  RouteSymbol+  %ESRI.ArcGIS.Client.Symbols.LineSymbol
   Color   Width  PanelGradient �� EndPoint �� 
StartPoint �� RelativeTransform �� CenterY �� CenterX �� CenterY �� CenterX �� Angle �� CenterY �� CenterX � Color � Offset��%�  &	        &	 ]    7   &	 �    &	 �    &	     �� 5      .R�5      � .+�5	      A� 
���   �$��1��6�   ��6   �� *Symbol.Color+��6<   �� *Symbol.Size+��6Z   �� *Symbol.Size+6�   �� 5
      $��Center=�6    $��Center=�6=   $��9(�5      $��0q�6    $
��Bold"�6+   
���   ���5      �� $ Attributes[StopNumber]�5   =   5      5        5      $ 20Q�6:   $ Circle=�6D   
 �r���8   6b     5       �	#660000FF
 ��  �$	 2Q�6\     5      
 �	#990000FF$ 5Q�6J   �� 5      $ 0.5,1&�68   $ 0.5,0&�6I    5      V� 5      .~�5      �� $	 0.5Q�6)   $	 0.5Q�67   �� 5      $	 0.5Q�6(   $	 0.5Q�66   �� 5      $	 176Q�6*   $	 0.5Q�66   $	 0.5Q�6D   T� 5      5      .��5      � $ 	#FF145787��6   $
 0.16Q�61   � 5      $ 	#FF3D7FAC��6   $ 0.502Q�61   � 5      $ 	#FF88C5EF��6   $ 0.984Q�61   5      5       � ColumnDefinitions 5    
   �� 5!      $	��89* �6   �� 5"      $	��21* �6   .+�5$   
     ESRI.ArcGIS.Client.Map  -   5          Name$ MyMap��5$      
�������   Extent3  -ESRI.ArcGIS.Client.Geometry.EnvelopeConverter$( "-122.456, 37.752, -122.389, 37.824 65   $��-184,0,0,-149q�5%   1   $��2��6H      Layers 5'      3	  -ESRI.ArcGIS.Client.ArcGISTiledMapServiceLayer	  
 	  Url$] Whttp://services.arcgisonline.com/ArcGIS/rest/services/ESRI_StreetMap_World_2D/MapServer��5(      &
   ESRI.ArcGIS.Client.GraphicsLayer
  5*        ESRI.ArcGIS.Client.Layer	   ID$ MyRouteGraphicsLayer��6%   
  5+      $ MyStopsGraphicsLayer��6%   
  5,      $ MyBlockGraphicsLayer��6%   5.      � 50   
   $��Right=�6   $	��Top=�6+   $��	0,10,10,0q�6C   $��2��6V   .+�51      �� #��[ 6   
������� �� RadiusX$ 10��6M    �� RadiusY$ 10��6Z   $��0q�6g     J� Effect  52      3  -System.Windows.Media.Effects.DropShadowEffect  53      55      �� 56      ! �� Orientation$! 
Horizontal=�6   $��5q�62   .+�57      	� -   5        " 	�Name$" StopsRadioButton��57   2   $��	Add Stops��6   # d� 	IsChecked$
# trueY�6L   
�������$ 	� 	GroupName$	$ add��58   ,   $��Center=�6<   6W   	� 59      -   5        $" BarriersRadioButton��59   5   $��Add Barriers��6   
�������$	$ add��5:   ,   $��Center=�6<   6W   �� 5;      -   5        $��Solve��5;      $��5,0,0,0q�6=   �� 5<      -   5        $��Clear��5<      $��5,0,0,0q�6=   5=      5>      5?      5@      