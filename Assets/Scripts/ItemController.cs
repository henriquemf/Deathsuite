using UnityEngine;
using UnityEngine.SceneManagement;

public class ItemController : MonoBehaviour
{
    public Animator animator;
    public Animator itemUIanimator;
    public Animator keyAnimator;
    public float amplitude = 0.1f;
    public float speed = 4f;
    public static Item itemGameObject;
    private GameObject itemObject;
    private Vector3 originalPosition;
    private SpriteRenderer spriteRenderer;
    private bool isInsideItemCollider = false;
    private Inventory inventory;
    private GameObject UI;
    private GameObject UI_child1;
    private GameObject UI_child2;
    private GameObject UI_child3;
    private GameObject UI_child4;
    private GameObject UI_child5;
    private GameObject UI_child6;
    private GameObject itemUI;
    private SpriteRenderer itemUIsprite;
    public StatsText statsText;
    public SpriteRenderer epress;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        itemUI = GameObject.Find("UISprite");
        itemUIsprite = itemUI.GetComponent<SpriteRenderer>();
        itemUIsprite.color = new Color(1f, 1f, 1f, 0f);

        UI = GameObject.Find("UI");
        UI_child1 = UI.transform.GetChild(0).gameObject;
        UI_child2 = UI.transform.GetChild(1).gameObject;
        UI_child3 = UI.transform.GetChild(2).gameObject;
        UI_child4 = UI.transform.GetChild(3).gameObject;
        UI_child5 = UI.transform.GetChild(4).gameObject;
        UI_child6 = UI.transform.GetChild(5).gameObject;

        ItemRandomizer itemRandomizer = FindObjectOfType<ItemRandomizer>();

        if (FirstTimeLoader.firstTimeLoading)
        {
            itemRandomizer.LoadItemList();
            FirstTimeLoader.firstTimeLoading = false;
        }

        itemGameObject = itemRandomizer.GetRandomItem();
        itemObject = GameObject.Find("Item");

        spriteRenderer = itemObject.GetComponent<SpriteRenderer>();
        Sprite itemSprite = itemGameObject.itemImage;
        spriteRenderer.sprite = itemSprite;
       
        originalPosition = itemObject.transform.position;
        animator = GetComponent<Animator>();

        animator.Play("Appear");
        Invoke("ActivateItem", animator.GetCurrentAnimatorStateInfo(0).length);
    }

    private void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Bouncing"))
        {
            float yPosition = originalPosition.y + amplitude * Mathf.Sin(speed * Time.time);
            itemObject.transform.position = new Vector3(originalPosition.x, yPosition, originalPosition.z);
        }

        if (isInsideItemCollider)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Adiciona o item ao inventário
                inventory = FindObjectOfType<Inventory>();
                if (inventory.AddItem(itemGameObject))
                {
                    audioSource.Play();
                    if (InventoryManager.inventoryItems.Count == 1)
                    {
                        UI_child1.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemGameObject.itemImage;
                    }
                    else if (InventoryManager.inventoryItems.Count == 2)
                    {
                        UI_child2.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemGameObject.itemImage;
                    }
                    else if (InventoryManager.inventoryItems.Count == 3)
                    {
                        UI_child3.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemGameObject.itemImage;
                    }
                    else if (InventoryManager.inventoryItems.Count == 4)
                    {
                        UI_child4.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemGameObject.itemImage;
                    }
                    else if (InventoryManager.inventoryItems.Count == 5)
                    {
                        UI_child5.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemGameObject.itemImage;
                    }
                    else if (InventoryManager.inventoryItems.Count == 6)
                    {
                        UI_child6.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = itemGameObject.itemImage;
                    }
                    // Atualiza os status do jogador
                    ChangePlayerStats();
                    // Se o item foi adicionado com sucesso, destrói o objeto de item
                    //Debug.Log(inventory.GetItems());
                    Destroy(gameObject);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ItemCollider"))
        {
            epress.enabled = true;
            keyAnimator.enabled = true;
            keyAnimator.Play("EPress");
            itemUIanimator.Play("UIAppear");
            statsText.GenerateText(itemGameObject);
            // Exibe a mensagem de confirmação
            //Debug.Log("Deseja pegar o item? Pressione E para confirmar.");

            isInsideItemCollider = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("ItemCollider"))
        {
            epress.enabled = false;
            keyAnimator.enabled = false;
            itemUIanimator.Play("UIDisappear");
            statsText.ClearText();
            // Esconde a mensagem de confirmação
            //Debug.Log("Saindo do alcance do item.");
            isInsideItemCollider = false;
        }
    }

    private void ChangePlayerStats() {
        //Debug.Log("Item " + itemGameObject.itemName + " ativado");
        //Debug.Log("Vida atual: " + PlayerCharacter.GetCurrentLifePoints());
        //Debug.Log("Ataque atual: " + PlayerCharacter.GetCurrentAttackPoints());
        //Debug.Log("Proficiência atual: " + PlayerCharacter.GetCurrentProficiencyPoints());
        //Debug.Log("Excelência atual: " + PlayerCharacter.GetCurrentExcelencePoints());

        if (itemGameObject.itemName == "VoidHelmet") 
        {
            PlayerCharacter.SetLifePoints(1);
            PlayerCharacter.AddAttackPoints(itemGameObject.itemAttackPoints);
            PlayerCharacter.AddProficiencyPoints(itemGameObject.itemProficiencyPoints);
            PlayerCharacter.AddExcelencePoints(itemGameObject.itemExcelencePoints);
        }
        else if (itemGameObject.itemName == "HeavensHelmet")
        {
            PlayerCharacter.SetLifePoints(PlayerCharacter.GetCurrentLifePoints() * 2);
            PlayerCharacter.SetAttackPoints(PlayerCharacter.GetCurrentAttackPoints() * 4/5);
        }
        else if (itemGameObject.itemName == "OrtraxRing")
        {
            foreach (Item item in InventoryManager.inventoryItems)
            {
                if (item.itemName == "OrtraxAmulet")
                {
                    PlayerCharacter.AddAttackPoints(PlayerCharacter.GetCurrentAttackPoints() + itemGameObject.itemAttackPoints * 2);
                    PlayerCharacter.AddLifePoints(PlayerCharacter.GetCurrentLifePoints() + itemGameObject.itemLifePoints * 2);
                    break;
                }
                else
                {
                    PlayerCharacter.AddAttackPoints(PlayerCharacter.GetCurrentAttackPoints() + itemGameObject.itemAttackPoints);
                    PlayerCharacter.AddLifePoints(PlayerCharacter.GetCurrentLifePoints() + itemGameObject.itemLifePoints);
                    break;
                }
            }
        }

        else
        {
            foreach (Item item in InventoryManager.inventoryItems)
            {
                if (item.itemName == "VoidHelmet")
                {
                    PlayerCharacter.SetLifePoints(1);
                    PlayerCharacter.AddAttackPoints(itemGameObject.itemAttackPoints);
                    PlayerCharacter.AddProficiencyPoints(itemGameObject.itemProficiencyPoints);
                    PlayerCharacter.AddExcelencePoints(itemGameObject.itemExcelencePoints);
                    break;
                }
                else
                {
                    PlayerCharacter.AddLifePoints(itemGameObject.itemLifePoints);
                    PlayerCharacter.AddAttackPoints(itemGameObject.itemAttackPoints);
                    PlayerCharacter.AddProficiencyPoints(itemGameObject.itemProficiencyPoints);
                    PlayerCharacter.AddExcelencePoints(itemGameObject.itemExcelencePoints);
                    break;
                }
            }
        }

        //Debug.Log("Vida atual: " + PlayerCharacter.GetCurrentLifePoints());
        //Debug.Log("Ataque atual: " + PlayerCharacter.GetCurrentAttackPoints());
        //Debug.Log("Proficiência atual: " + PlayerCharacter.GetCurrentProficiencyPoints());
        //Debug.Log("Excelência atual: " + PlayerCharacter.GetCurrentExcelencePoints());
    }

    private void ActivateItem()
    {
        animator.Play("Bouncing");
    }
}
