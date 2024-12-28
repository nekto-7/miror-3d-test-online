using UnityEngine;
using Mirror;

public class MoveController : NetworkBehaviour
{
    public float gravity = -9.8f;
    public float speed = 15.0f;
    public float sprintSpeed = 20.0f;
    public float jumpForce = 15.0f;
    public bool CanMove = true;

    public Transform cameraTransform; // Ссылка на камеру (локального игрока)

    private CharacterController cc;
    private float jspeed = 0.0f;
    private float currentSpeed; // Текущая скорость

    private void Awake()
    {
        cc = GetComponent<CharacterController>();
        currentSpeed = speed; 
    }

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
 
        if (cameraTransform != null)
        {
            cameraTransform.gameObject.SetActive(true);
        }
    }

    private void Update()
    {
        // Проверяем, является ли это локальный игрок
        if (!isLocalPlayer || !CanMove) return;

        // Обновляем движение
        Vector3 movement = CalculateMovement();

        // Применяем гравитацию
        ApplyGravity(ref movement);

        // Двигаем игрока
        cc.Move(movement * Time.deltaTime);
    }

    private Vector3 CalculateMovement()
    {
        // Считываем ввод игрока
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        // Рассчитываем движение в локальных координатах
        Vector3 direction = new Vector3(horizontal, 0, vertical);

        // Нормализация для плавности движения
        if (direction.magnitude > 1)
            direction.Normalize();

        // Преобразование направления в мировые координаты относительно камеры
        Vector3 forward = cameraTransform.forward; // Направление вперед
        Vector3 right = cameraTransform.right;     // Направление вправо

        // Убираем вертикальную составляющую
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        // Итоговое направление движения
        Vector3 moveDirection = forward * vertical + right * horizontal;

        // Обработка спринта
        if (Input.GetKey(KeyCode.LeftShift))
        {
            currentSpeed = sprintSpeed;
        }
        else
        {
            currentSpeed = speed;
        }

        // Применяем текущую скорость
        return moveDirection * currentSpeed;
    }

    private void ApplyGravity(ref Vector3 movement)
    {
        if (cc.isGrounded)
        {
            jspeed = 0;
            if (Input.GetKeyDown(KeyCode.Space))
            {
                jspeed = jumpForce; // Прыжок
            }
        }

        jspeed += gravity * Time.deltaTime; // Применение гравитации
        movement.y = jspeed; // Добавление вертикального движения
    }
}
