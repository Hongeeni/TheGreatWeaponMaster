using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player_Control_Script : MonoBehaviour
{
    public GameObject playerCameraAngle;
    public GameObject playerHealthBar;
    public GameObject playerWeapon;
    public TrailRenderer trailRenderer;

    private Rigidbody characterRigidbody;
    private Animator characterAnimation;
    private Image playerHealthBarImage;
    private MeshCollider weaponCollider;

    private AudioSource musicPlayer;

    private float playerSpeed = 1000.0f;
    private float playerJumpForce = 5000.0f;

    private float horizontalAxis;
    private float verticalAxis;

    private Vector3 moveVec;

    private float playerXRotateSpeed = 2.5f;
    private float playerYRotateSpeed = 7.0f;

    private float playerYCameraRotate = 0f;
    private float playerCurrentCameraRotate = 0f;
    private float playerRotateLimit = 60.0f;

    private bool canDamage = true;
    private bool canHeal = true;
    private bool canLand = false;

    GameObject nearObject;
    GameObject equipWeapon;
    int equipWeaponIndex = -1;
    public GameObject[] weapons;
    public bool[] hasWeapons;

    bool iDown;
    bool sDown1;
    bool sDown2;
    bool sDown3;

    private void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start()
    {
        characterRigidbody = this.GetComponent<Rigidbody>();
        characterAnimation = this.GetComponent<Animator>();

        playerHealthBarImage = playerHealthBar.GetComponent<Image>();
        weaponCollider = playerWeapon.GetComponent<MeshCollider>();

        musicPlayer = this.GetComponent<AudioSource>();
    }

    private void Update()
    {   
        GetInput();
        Swap();
        Interation();

        if (Time.timeScale != 0)
        {
            CameraRotate();
            Move();
            Jump();
            Attack();
        }
    }

    void GetInput()
    {
        iDown = Input.GetButtonDown("Interation");
        sDown1 = Input.GetButtonDown("Swap1");
        sDown2 = Input.GetButtonDown("Swap2");
        sDown3 = Input.GetButtonDown("Swap3");
    }

    private void FixedUpdate()
    {
        PreformRotation();

        if (!characterAnimation.GetBool("Jump") && !characterAnimation.GetBool("Attack"))
        {
            if (moveVec != Vector3.zero && !characterAnimation.GetBool("Die"))
            {
                characterAnimation.SetBool("MoveCheck", true);
                characterRigidbody.AddForce(this.gameObject.transform.localRotation * moveVec * playerSpeed);
            }
            else
            {
                characterAnimation.SetBool("MoveCheck", false);
            }
        }
        else if (characterAnimation.GetBool("Jump") && !characterAnimation.GetBool("Attack"))
        {
            if (moveVec != Vector3.zero && !characterAnimation.GetBool("Land") && !characterAnimation.GetBool("Die"))
            {
                characterRigidbody.AddForce(this.gameObject.transform.localRotation * moveVec * playerSpeed);
            }
        }
    }

    private void PreformRotation()
    {
        playerCurrentCameraRotate -= playerYCameraRotate;
        playerCurrentCameraRotate = Mathf.Clamp(playerCurrentCameraRotate, -playerRotateLimit, playerRotateLimit);
        playerCameraAngle.transform.localEulerAngles = new Vector3(playerCurrentCameraRotate, 0f, 0f);
    }

    private void CameraRotate()
    {
        float xRot = Input.GetAxis("Mouse X") * playerXRotateSpeed;
        this.transform.localRotation *= Quaternion.Euler(0f, xRot, 0f);

        playerYCameraRotate = Input.GetAxis("Mouse Y") * playerYRotateSpeed;
    }

    private void Move()
    {
        horizontalAxis = Input.GetAxisRaw("Horizontal");
        verticalAxis = Input.GetAxisRaw("Vertical");

        moveVec = new Vector3(horizontalAxis, 0, verticalAxis).normalized;

        characterAnimation.SetFloat("MoveX", moveVec.x);
        characterAnimation.SetFloat("MoveZ", moveVec.z);
    }

    private void Jump()
    {
        if (!characterAnimation.GetBool("Jump") && !characterAnimation.GetBool("Attack"))
        {
            if (Input.GetKeyDown(KeyCode.Space) && !characterAnimation.GetBool("Die"))
            {
                //플레이어 캐릭터의 이동 정지
                characterAnimation.SetBool("MoveCheck", false);

                //플레이어 점프 코루틴 실행
                StartCoroutine(PlayerJump());
            }
        }
    }

    private void Attack()
    {
        if (!characterAnimation.GetBool("Jump") && !characterAnimation.GetBool("Attack"))
        {
            if (Input.GetMouseButtonDown(0) && !characterAnimation.GetBool("Die"))
            {
                StartCoroutine(AttackAnimation());
            }
        }
    }

    private void TakeDamage(float damageAmount)
    {
        playerHealthBarImage.fillAmount -= damageAmount;
        if (playerHealthBarImage.fillAmount <= 0)
        {
            characterAnimation.SetBool("Die", true);
        }
    }

    void Swap()
    {
        //if(sDown1 && (!hasWeapons[0] || equipWeaponIndex == 0)) //무기가 없거나 들고있는 무기일 경우
        //    return;
        //if(sDown2 && (!hasWeapons[1] || equipWeaponIndex == 1)) //무기가 없거나 들고있는 무기일 경우
        //    return;
        //if(sDown3 && (!hasWeapons[2] || equipWeaponIndex == 2)) //무기가 없거나 들고있는 무기일 경우
        //    return;
        //int weaponIndex = -1;
        //if(sDown1) weaponIndex = 0;
        //if(sDown2) weaponIndex = 1;
        //if(sDown3) weaponIndex = 2;
        //if(sDown1 || sDown2 || sDown3)
        //{
        //    if(equipWeapon != null) //빈손
        //        equipWeapon.SetActive(false);
        //    equipWeaponIndex = weaponIndex;
        //    equipWeapon = weapons[weaponIndex];
        //    equipWeapon.SetActive(true);
        //}
    }
    
    void Interation()
    {
        if(iDown && nearObject!=null){
            if(nearObject.tag == "Weapon"){
                Item item = nearObject.GetComponent<Item>();
                int weaponIndex = item.value;
                hasWeapons[weaponIndex] = true;
                Debug.Log(nearObject.name);
                Destroy(nearObject);
                if(equipWeapon != null) //빈손
                    equipWeapon.SetActive(false);
                equipWeaponIndex = weaponIndex;
                equipWeapon = weapons[weaponIndex];
                equipWeapon.SetActive(true);
                playerWeapon = equipWeapon;
                trailRenderer = equipWeapon.transform.Find("Effect").GetComponent<TrailRenderer>();

                ClearStage_Script.getSword = true;
            }
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (other.tag == "HealArea")
            StartCoroutine(PlayerHeal());

        if (other.tag == "Weapon")
            nearObject = other.gameObject;
    }

    void OnTriggerExit(Collider other)
    {
        if(other.tag == "Weapon")
            nearObject = null;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" && canLand && !characterAnimation.GetBool("Die"))
            StartCoroutine(LandingAnimation());

        if (collision.gameObject.tag == "Boss")
            StartCoroutine(PlayerDamage(0.12f));
        else if (collision.gameObject.tag == "FireBall")
        {
            StartCoroutine(PlayerDamage(0.1f));
            Destroy(collision.gameObject);
        }
        else if (collision.gameObject.tag == "MiddleBoss")
            StartCoroutine(PlayerDamage(0.1f));
        else if (collision.gameObject.tag == "Enemy")
            StartCoroutine(PlayerDamage(0.05f));

    }

    private IEnumerator AttackAnimation()
    {
        //플레이어 캐릭터의 이동 정지
        characterAnimation.SetBool("MoveCheck", false);

        characterAnimation.SetBool("Attack", true);
        musicPlayer.Play();

        weaponCollider.enabled = true;
        trailRenderer.enabled = true;
        yield return new WaitForSeconds(1.0f);
        weaponCollider.enabled = false;
        trailRenderer.enabled = false;
        characterAnimation.SetBool("Attack", false);

        playerWeapon.GetComponent<WeaponDamage>().takeDamage = false;
    }

    private IEnumerator LandingAnimation()
    {
        characterAnimation.SetBool("Land", true);
        yield return new WaitForSeconds(0.2f);
        characterAnimation.SetBool("Land", false);
        characterAnimation.SetBool("Jump", false);

        canLand = false;
    }

    private IEnumerator PlayerHeal()
    {
        if (canHeal && playerHealthBarImage.fillAmount > 0 && playerHealthBarImage.fillAmount < 1)
        {
            playerHealthBarImage.fillAmount += 0.1f;
            canHeal = false;
            yield return new WaitForSeconds(1.0f);
            canHeal = true;
        }
    }

    private IEnumerator PlayerDamage(float damageAmount)
    {
        if (canDamage)
        {
            TakeDamage(damageAmount);
            canDamage = false;
            yield return new WaitForSeconds(0.5f);
            canDamage = true;
        }
    }

    private IEnumerator PlayerJump()
    {
        if (!canLand)
        {
            //플레이어 캐릭터 점프
            characterAnimation.SetBool("Jump", true);
            characterRigidbody.AddForce(this.transform.up * playerJumpForce);

            yield return new WaitForSeconds(0.2f);
            canLand = true;
        }
    }
}
