using UnityEngine;

public class BackgroundManager : MonoBehaviour {
    public GameObject[] backgrounds;
    public BackgroundUnderControl[] backgroundsUnder;

    public void sortBackgrounds() {
        int minIndex = 0, maxIndex = 0;

        for(int i = 0; i < backgrounds.Length; i++) {
            if (backgrounds[minIndex].transform.position.x > backgrounds[i].transform.position.x)
                minIndex = i;
            else if (backgrounds[maxIndex].transform.position.x < backgrounds[i].transform.position.x)
                maxIndex = i;
        }

        GameObject[] sorted = new GameObject[3];
        for (int i = 0; i < backgrounds.Length; i++) 
            if (!backgrounds[i].Equals(backgrounds[minIndex]) && !backgrounds[i].Equals(backgrounds[maxIndex])) {
                sorted[0] = backgrounds[minIndex];
                sorted[1] = backgrounds[i];
                sorted[2] = backgrounds[maxIndex];
                break;
            }
        backgrounds = sorted;
    }

    public void sortBackgroundsUnder(Vector3 position, int index, int direction) {
        for (int i = 0; i < backgroundsUnder.Length; i++) {
            if (backgroundsUnder[i].positionIndex.Equals(index)) {
                for (int j = 0; j < backgroundsUnder.Length; j++) {
                    if (backgroundsUnder[j].positionIndex.Equals(direction)) {
                        int temp = backgroundsUnder[i].positionIndex;
                        backgroundsUnder[i].positionIndex = backgroundsUnder[j].positionIndex;
                        backgroundsUnder[j].positionIndex = temp;
                        break;
                    }
                }
                    break;
            }
        }

    }
}
