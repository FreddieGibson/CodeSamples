import arcgisscripting,os
arcpy = arcgisscripting.create(9.3)

def SimplifyPath(path, length):
   parts = str.split(path, os.sep); parts.reverse()
   xPath = [parts[i] for i in range(length)]; xPath.reverse()
   return r'..' + os.sep + os.sep.join(xPath)

def main():
   try:
      print(r'Adding toolbox %s' % SimplifyPath(r'C:\Program Files (x86)\ArcGIS\ArcPad10.2\DesktopTools10.0\Toolboxes\ArcPad Tools.tbx', 4))
      arcpy.AddToolbox(r'C:\Program Files (x86)\ArcGIS\ArcPad10.2\DesktopTools10.0\Toolboxes\ArcPad Tools.tbx')
      try:
         arcpy.ArcPadCheckout_ArcPad(r'C:\Uzumaki\Incidents\1281269_TrimbleArcPad\EngineArcPadApp\EngineArcPadApp\bin\Debug\data\Riverside.gdb\Parcel;C:\Uzumaki\Incidents\1281269_TrimbleArcPad\EngineArcPadApp\EngineArcPadApp\bin\Debug\data\Riverside.gdb\Streets', '#', '#', '#', r'C:\Uzumaki\Incidents\1281269_TrimbleArcPad\EngineArcPadApp\EngineArcPadApp\bin\Debug\data\Riverside.axf')
         print(arcpy.GetMessages(0))
      except arcpy.ExecuteError: print(arcpy.GetMessages(2))
      except: print('Generic Exception')
      finally: arcpy.RemoveToolbox(r'C:\Program Files (x86)\ArcGIS\ArcPad10.2\DesktopTools10.0\Toolboxes\ArcPad Tools.tbx')
   except: print('Error Adding Toolbox')

if __name__ == '__main__':
   main()
