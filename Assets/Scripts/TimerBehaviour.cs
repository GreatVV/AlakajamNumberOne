using Entitas.CodeGeneration.Attributes;
using TMPro;

[Game, Unique]
public class TimerBehaviour : InjectableBehaviour
{
    public TextMeshProUGUI TimerText;
    
    protected override void OnInject()
    {
        _contexts.game.SetTimerBehaviour(this);
    }

    public void SetSeconds(float time)
    {
        TimerText.text = string.Format("Boss spawns in {0:0}s", time);
    }

    public void Hide()
    {
        TimerText.text = "Boss is spawned";
    }
}