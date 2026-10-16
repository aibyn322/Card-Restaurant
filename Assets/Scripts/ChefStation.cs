using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ChefStation : MonoBehaviour, IDropHandler, IPointerClickHandler
{
    public List<Recipe> recipes;
    public GameObject cardPrefab;
    public Transform cardParent;
    public Image progressBar;
    public TMP_Text stackLabel;

    readonly List<CardData> stack = new List<CardData>();
    bool busy;

    public void OnPointerClick(PointerEventData e)
{
    if (busy) return;
    stack.Clear();
    stackLabel.text = "";
}
    public void OnDrop(PointerEventData e)
    {
        if (busy || e.pointerDrag == null) return;

        var view = e.pointerDrag.GetComponent<CardView>();
        if (view == null) return;

        stack.Add(view.data);              // remember the ingredient
        Destroy(view.gameObject);          // the card "goes into" the chef
        stackLabel.text = string.Join("\n", stack.Select(c => c.cardName));

        Recipe match = FindMatch();
        if (match != null) StartCoroutine(Craft(match));
    }

    Recipe FindMatch()
    {
        foreach (var r in recipes)
        {
            if (r.ingredients.Count != stack.Count) continue;
            bool same = r.ingredients.OrderBy(x => x.name)
                         .SequenceEqual(stack.OrderBy(x => x.name));
            if (same) return r;
        }
        return null;
    }

    IEnumerator Craft(Recipe r)
    {
        busy = true;
        float t = 0f;
        while (t < r.craftTime)
        {
            t += Time.deltaTime;
            progressBar.fillAmount = t / r.craftTime;
            yield return null;             // wait one frame
        }

        var card = Instantiate(cardPrefab, transform.position + new Vector3(0, -250, 0),
                               Quaternion.identity, cardParent);
        card.GetComponent<CardView>().Setup(r.result);

        stack.Clear();
        stackLabel.text = "";
        progressBar.fillAmount = 0f;
        busy = false;
    }
}