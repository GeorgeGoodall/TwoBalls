using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallImage
{ 
    Sprite image;
    public int rowWidth = 5;
    int currentRow = 0;


    public static Dictionary<Color, WallSpawner.WallTypes> colorToWallType {get; private set;} = new Dictionary<Color, WallSpawner.WallTypes>(){
        {new Color(0,1,0,1),WallSpawner.WallTypes.GRABBABLE},
        {new Color(1,1,0,1),WallSpawner.WallTypes.SAND},
        {new Color(0,0,0,1),WallSpawner.WallTypes.BLACKHOLE},
        {new Color(1,0,0,1),WallSpawner.WallTypes.IMPASSABLE}
    };

    public bool hasFinished(){
        return currentRow >= image.texture.height;
    }

    public WallImage(Sprite _image){
        image = _image;
        rowWidth = image.texture.width;
        
    }

    public int getHeight(){
        return image.texture.height;
    }

    public void updateViewWidth(){
        Camera.main.GetComponent<CameraScreenResolution>().setWidth(rowWidth);
    }

    public WallSpawner.WallTypes[] getRow(int rowNumber){
        
        WallSpawner.WallTypes[] row = new WallSpawner.WallTypes[rowWidth];
        
        for (int i = 0; i < rowWidth; i++)
        {
            Color pix = image.texture.GetPixel(i,rowNumber);

            if(colorToWallType.ContainsKey(pix)){
                row[i] = colorToWallType[pix];
                continue;
            }
            row[i] = WallSpawner.WallTypes.Empty;

        }

        return row;
    }

    public WallSpawner.WallTypes[] getCurrentRow(){
        WallSpawner.WallTypes[] toReturn = getRow(currentRow);
        currentRow++;
        return toReturn;
    }

}