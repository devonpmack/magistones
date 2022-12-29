using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler {
  private bool dragging = false;
  private bool mouseOver = false;
  private Vector2 mouseOffset;
  public GameObject placeholder;
  public Shop shop;
  public string abilityName;


  public bool isDraggable = true;

  public void onDragStart() {
    if (!isDraggable) return;
    placeholder = transform.parent.Find("placeholder").gameObject;

    placeholder.SetActive(true);
    dragging = true;
    transform.SetAsLastSibling();

    shop.sellIndicator.SetActive(true);
  }

  public void onDragEnd() {
    shop.sellIndicator.SetActive(false);
    placeholder.SetActive(false);
    dragging = false;

    var index = getIndexForPos(transform.localPosition);
    if (index == -1) {
      shop.sell(transform, abilityName);
    } else {
      transform.SetSiblingIndex(index);

      Debug.Log("reorder " + abilityName + " to " + index);
      shop.reorderAbility(abilityName, index);
    }
  }

  private int getIndexForPos(Vector2 pos) {
    if (pos.x > 300) {
      return -1;
    }

    // update sibling index from 0 to 3 based on position, each slot is 190px wide
    if (pos.x + 140 / 2 < -133) {
      return 0;
    } else if (pos.x + 140 / 2 < 6) {
      return 1;
    } else if (pos.x + 140 / 2 < 146) {
      return 2;
    } else {
      return 3;
    }
  }

  void Update() {
    if (Input.GetMouseButtonDown(0) && mouseOver) {
      onDragStart();
      mouseOffset = new Vector2(Input.mousePosition.x - transform.position.x, Input.mousePosition.y - transform.position.y);
    }

    if (dragging) {
      transform.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0) - (Vector3)mouseOffset;
      placeholder.transform.SetSiblingIndex(getIndexForPos(transform.localPosition));
    }

    if (Input.GetMouseButtonUp(0) && dragging) {
      onDragEnd();
    }
  }

  public void OnPointerEnter(PointerEventData eventData) {
    mouseOver = true;
  }

  public void OnPointerExit(PointerEventData eventData) {
    mouseOver = false;
  }
}
