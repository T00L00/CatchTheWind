using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CTW.UI
{
    public class UIManager : MonoBehaviour
    {
        // Text References
        [SerializeField] TextMeshProUGUI PlayerHealthText;
        [SerializeField] TextMeshProUGUI TreeCountText;
        [SerializeField] TextMeshProUGUI ForestHealthText;

        // Sapling reference
        [SerializeField] Sapling sapling;
        private const string HEALTH_UI_TEXT = "Health: ";

        // More Forest Health related stuff
        [SerializeField] Slider ForestHealthBar;
        [SerializeField] Gradient ForestBarGradient;
        [SerializeField] Image ForestBarImage;
        [SerializeField] float DepletionRate;

        // Tree Count
        [SerializeField] int ForestHealthIncreasePerTree = 20;
        private int TotalTreeCount;
        private int CurrentTreeCount;        

        TreeSpot[] treeSpots;

        private void Start()
        {
            GetTreeSpots();

            // Player Health
            PlayerHealthText.text = HEALTH_UI_TEXT + sapling.health.ToString();

            // Tree Count
            TreeCountText.text = CurrentTreeCount.ToString() + "/" + TotalTreeCount.ToString();

            // Forest Health
            ForestHealthBar.value = 100;
            ForestBarImage.color = ForestBarGradient.Evaluate(1f);
            StartCoroutine(decreaseForestHealth(DepletionRate));
        }

        // Color gradient not working currently
        private IEnumerator decreaseForestHealth(float amount)
        {
            while (true)
            {
                //  Debug.Log("Yoo");
                ForestHealthBar.value -= amount;
                yield return new WaitForSeconds(0.1f);
                ForestBarImage.color = ForestBarGradient.Evaluate(ForestHealthBar.normalizedValue);

                if (ForestHealthBar.value < 30f)
                    ForestHealthText.enabled = true;
                else
                    ForestHealthText.enabled = false;
            }
        }

        private void OnEnable()
        {
            // Subscribe UI to Health monitoring event
            sapling.ChangedHealth += OnChangedHealth;
        }

        private void OnDisable()
        {
            sapling.ChangedHealth -= OnChangedHealth;
        }

        private void OnChangedHealth()
        {
            PlayerHealthText.text =  HEALTH_UI_TEXT + sapling.health.ToString();
        }

        private void GetTreeSpots()
        {
            GameObject[] treeSpotsGO = GameObject.FindGameObjectsWithTag("TreeSpot");
            treeSpots = new TreeSpot[treeSpotsGO.Length];
            for (int i = 0; i < treeSpotsGO.Length; i++)
            {
                treeSpots[i] = treeSpotsGO[i].GetComponent<TreeSpot>();
                treeSpots[i].OnFinishGrow += OnFinishTreeGrow;
            }

            TotalTreeCount = treeSpots.Length;
        }

        private void OnFinishTreeGrow()
        {
            CurrentTreeCount += 1;
            TreeCountText.text = CurrentTreeCount.ToString() + "/" + TotalTreeCount.ToString();
            ForestHealthBar.value += ForestHealthIncreasePerTree;
        }
    }

}
