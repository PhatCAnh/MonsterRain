using UnityEngine;
using UnityEngine.EventSystems;

public class GunController : MonoBehaviour
{
    [SerializeField] private Animator gunAnim;
    [SerializeField] private Transform gun;
    [SerializeField] private float gunDistance = 1.2f;

    private bool gunFacingRight = true;

    [Header("Bullet")]
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private int maxBullets = 15;
    public int currentBullets;
    

    private void Start()
    {
        ReloadGun();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePos - transform.position;

        gun.rotation = Quaternion.Euler(new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg));

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        gun.position = transform.position + Quaternion.Euler(0, 0, angle) * new Vector3(gunDistance, 0, 0);

        if (Input.GetKeyDown(KeyCode.Mouse0) && HaveBullets())
        {
            Shoot(direction);
        }

        GunFlipController(mousePos);

    }

    private void GunFlipController(Vector3 mousePos)
    {
        if (mousePos.x < gun.position.x && gunFacingRight)
        {
            gunFlip();
        }
        else if (mousePos.x > gun.position.x && !gunFacingRight)
        {
            gunFlip();
        }
    }

    private void gunFlip()
    {
        gunFacingRight = !gunFacingRight;

        gun.localScale = new Vector3(gun.localScale.x, gun.localScale.y * -1, gun.localScale.z);
    }

    private void Shoot(Vector3 direction)
    {
        // if (EventSystem.current.IsPointerOverGameObject())
        // {
        //     return;
        // }
        
        gunAnim.SetTrigger("Shoot");

        UIController.Instance.UpdateInfoAmmo(currentBullets, maxBullets);

        GameObject newBullet = Instantiate(bulletPrefab, gun.position, Quaternion.identity);
        //newBullet.GetComponent<Bullet>().Init(direction, this.bulletSpeed);

        Destroy(newBullet, 5);
    }

    public void ReloadGun()
    {
        currentBullets = maxBullets;
        UIController.Instance.UpdateInfoAmmo(currentBullets, maxBullets);
        Time.timeScale = 1;
    }

    private bool HaveBullets()
    {
        if (currentBullets <= 0)
        {
            return false;
        }

        currentBullets--;
        
        return true;
    }
}
