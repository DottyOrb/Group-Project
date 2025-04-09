using UnityEngine;
using UnityEngine.UI;
#if UNITY_EDITOR
using UnityEditor;
#endif




[ExecuteInEditMode()]
public class ProgressBar : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/UI/Linear Progress Bar")]
    public static void AddLinearProgressBar()
    {
        GameObject obj = Instantiate(Resources.Load<GameObject>("UI/Linear Progress Bar"));
        obj.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif

    public int Minimum; // Maximum Score
    public int Maximum; // Minimum Score
    public int Current; // Current Score
    public Image Mask;
    public Image Fill;
    public Color Color;
    public Score scoreScript;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        GetCurrentFill();
        Current = scoreScript.score;
    }

    void GetCurrentFill()
    {
     
        float currentOffset = Current - Minimum;
        float maximumOffset = Maximum - Minimum;
        float fillAmount = currentOffset / maximumOffset;
        Mask.fillAmount = fillAmount; // Takes the number score and refers that to the progress bar

        Fill.color = Color;
    }

}
