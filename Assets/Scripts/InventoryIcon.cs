using TMPro;
using UnityEngine;
using UnityEngine.UI;

[Game]
public class InventoryIcon : MonoBehaviour
{
    public RawImage Image;
    public Image ImageSprite;

    public TextMeshProUGUI AmountLabel;
    
    [SerializeField]
    private GameObject _chosen;

    private LayoutGroup _layoutGroup;

    private void UpdateLayout()
    {
        if (!_layoutGroup)
        {
            _layoutGroup = GetComponentInParent<LayoutGroup>();
        }

        if (_layoutGroup)
        {
            _layoutGroup.enabled = false;
            _layoutGroup.enabled = true;
        }
    }

    private int _amount = 0;

    public void IsChosen(bool state)
    {
        if (_chosen != null)
        {
            _chosen.SetActive(state);
        }
        else
        {
            Debug.LogWarning("No check mark for " + gameObject);
        }
    }

    public void Set(Texture texture)
    {
        Image.texture = texture;
    }

    public void Set(Sprite sprite)
    {
        ImageSprite.sprite = sprite;
    }

    public void SetAmount(int amount, int from = -1)
    {
        if (from < 0)
        {

            if (amount <= 0)
            {
                gameObject.SetActive(false);
            }
            else
            {
                gameObject.SetActive(true);
            }

            if (amount <= 1)
            {
                AmountLabel.gameObject.SetActive(false);
            }
            else
            {
                AmountLabel.gameObject.SetActive(true);

                AmountLabel.text = amount.ToString();
            }
        }
        else
        {
            AmountLabel.gameObject.SetActive(true);
            gameObject.SetActive(true);
            AmountLabel.text = string.Format("{0}/{1}", amount, from);
        }

        UpdateLayout();
    }

    public void AddAmount()
    {
        _amount++;
        SetAmount(_amount);
    }

    public void DecreaseAmount()
    {
        _amount--;
        SetAmount(_amount);
    }
}