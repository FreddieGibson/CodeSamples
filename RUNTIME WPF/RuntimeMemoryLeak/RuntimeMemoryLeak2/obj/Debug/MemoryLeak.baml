   M S B A M L   `   `   `  ����  RuntimeMemoryLeak2#    RuntimeMemoryLeak2.MemoryLeak   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ` \ESRI.ArcGIS.Client.Local, Version=10.2.3.0, Culture=neutral, PublicKeyToken=29c6dd6e8553d944Z VESRI.ArcGIS.Client, Version=10.2.3.0, Culture=neutral, PublicKeyToken=29c6dd6e8553d9447esri*http://schemas.esri.com/arcgis/client/2009   5      .��5	      � -   5          �Name$  
LayoutRoot��5	       � 	Resources 5
   
      
ComboStyle �� 
TargetType �� Property �� Value��%}   &	        �� 5      " ��6'   .�5      ��  � ��$ Right=�6?   �� 5       � ��$ Center=�6=   5      5      .+�5   
     ESRI.ArcGIS.Client.Map  -   5          Name$ MyMap��5         UseAcceleratedDisplay .    
WrapAround .    Layers. 5      3  -ESRI.ArcGIS.Client.ArcGISTiledMapServiceLayer    ESRI.ArcGIS.Client.Layer		   ID$	 World Topo Map��6.   

   Url$T
 Nhttp://services.arcgisonline.com/ArcGIS/rest/services/World_Topo_Map/MapServer��6B   &   ESRI.ArcGIS.Client.GraphicsLayer  5      $	 MyGraphicsLayer��6!   5      � 5   
   $
��Left=�6   $	��Top=�6*   $��
15,15,15,0q�6B   $	��275��6V   .+�5      �� 
�������
�������$��0,0,0,5q�64    J� Effect 5      3  -System.Windows.Media.Effects.DropShadowEffect  5      5      �� 5      ���	#DDFFFFFF
�������$��5,5,5,10q�6;   �� 5       �� Orientation$ Vertical=�6   $��10,10,10,10q�60   $
��Left=�6E   .+�5      �� $f��`Use the options below to change the number of satellites and the update speed of the satellites.��6   $
��Wrap=�5      $��Center=�6   
���   �$��5,0,5,0q�6J   �� 5       $ 
Horizontal=�6   $��5,0,5,0q�66   $
��Left=�6G   .+�5!      �� $��# of Vertices Per Square:��6    $	��155��6A   $��Stretch=�6M   
���   �$��5,5,5,0q�6|   �� 5"      -   5         ��Name$ NumberOfVertices��5"      $
��auto��69   $��75��6G   #��[  6R   $��0,5,5,5q�6v   6�   5#      5$      5%      5&      5'      