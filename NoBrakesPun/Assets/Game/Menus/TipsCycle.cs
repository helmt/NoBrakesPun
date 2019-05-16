using TMPro;
using UnityEngine;
using Random = System.Random;

public class TipsCycle : MonoBehaviour
{
    private TextMeshProUGUI text;
    private Random rng;
    private float countdown;
    private string[] tips =
    {
        "If you get your front wheel stuck after a crash, press [F] or [B] to yank your bike forwards or backwards respectively.",
        "Watch out for incoming traffic, colliding too strongly with a car or a wall could destroy your package!",
        "Press [TAB] to hide or unhide the leaderboard.", 
        "The slider in the top-right corner allows you to zoom in or out of the minimap.",
        "Press [LSHIFT] to drift while taking tight corners.",
        "Be wary of your speed : go too fast and you'll have trouble slowing down!",
        "Hopping repeatedly by pressing [SPACE] will slow you down a bit.",
        "Cut across the bridge in central park for a neat shortcut, and admire the glorious waving " +
        "statue of Charbelia's supreme leader.",
        "Press [V] to cycle between camera views."
    };

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        rng = new Random();
        countdown = 5f;
        text.text = tips[rng.Next(9)];
    }

    private void Update()
    {
        countdown -= Time.deltaTime;
        if (countdown <= 0f)
        {
            string next;
            do
            {
                next = tips[rng.Next(9)];
            } while (next == text.text);

            text.text = next;
            countdown = 5f;
        }

    }
}
