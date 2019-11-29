const MEMBERS = [
{"signature":"CameraMover","childcount":5,"type":"Class","name":"CameraMover","token":"public class CameraMover : MonoBehaviour ","scope":""},{"signature":"CameraMover.target","childcount":0,"type":"Field","name":"target","token":"public Transform target ","scope":"CameraMover"},{"signature":"CameraMover.movementSpeed","childcount":0,"type":"Field","name":"movementSpeed","token":"public float movementSpeed ","scope":"CameraMover"},{"signature":"CameraMover.Start()","childcount":0,"type":"Method","name":"Start","token":"void Start()","scope":"CameraMover"},{"signature":"CameraMover.Update()","childcount":0,"type":"Method","name":"Update","token":"void Update()","scope":"CameraMover"},{"signature":"CameraMover.FixedUpdate()","childcount":0,"type":"Method","name":"FixedUpdate","token":"private void FixedUpdate()","scope":"CameraMover"},{"signature":"CameraTriggerEvent","childcount":5,"type":"Class","name":"CameraTriggerEvent","token":"public class CameraTriggerEvent : MonoBehaviour ","scope":""},{"signature":"CameraTriggerEvent.loseText","childcount":0,"type":"Field","name":"loseText","token":"public GameObject loseText ","scope":"CameraTriggerEvent"},{"signature":"CameraTriggerEvent.cameraMover","childcount":0,"type":"Field","name":"cameraMover","token":"public CameraMover cameraMover ","scope":"CameraTriggerEvent"},{"signature":"CameraTriggerEvent.Start()","childcount":0,"type":"Method","name":"Start","token":"void Start()","scope":"CameraTriggerEvent"},{"signature":"CameraTriggerEvent.Update()","childcount":0,"type":"Method","name":"Update","token":"void Update()","scope":"CameraTriggerEvent"},{"signature":"CameraTriggerEvent.OnTriggerExit2D(Collider2D)","childcount":0,"type":"Method","name":"OnTriggerExit2D","token":"private void OnTriggerExit2D(Collider2D collision)","scope":"CameraTriggerEvent","params":{"collision":"Collider2D"}},{"signature":"CharacterController2D","childcount":22,"type":"Class","name":"CharacterController2D","token":"public class CharacterController2D : MonoBehaviour ","scope":""},{"signature":"CharacterController2D.m_JumpForce","childcount":0,"type":"Field","name":"m_JumpForce","token":"[SerializeField]\nprivate float m_JumpForce ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_CrouchSpeed","childcount":0,"type":"Field","name":"m_CrouchSpeed","token":"[Range(0, 1)]\n[SerializeField]\nprivate float m_CrouchSpeed ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_MovementSmoothing","childcount":0,"type":"Field","name":"m_MovementSmoothing","token":"[Range(0, .3f)]\n[SerializeField]\nprivate float m_MovementSmoothing ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_AirControl","childcount":0,"type":"Field","name":"m_AirControl","token":"[SerializeField]\nprivate bool m_AirControl ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_WhatIsGround","childcount":0,"type":"Field","name":"m_WhatIsGround","token":"[SerializeField]\nprivate LayerMask m_WhatIsGround ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_GroundCheck","childcount":0,"type":"Field","name":"m_GroundCheck","token":"[SerializeField]\nprivate Transform m_GroundCheck ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_CeilingCheck","childcount":0,"type":"Field","name":"m_CeilingCheck","token":"[SerializeField]\nprivate Transform m_CeilingCheck ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_CrouchDisableCollider","childcount":0,"type":"Field","name":"m_CrouchDisableCollider","token":"[SerializeField]\nprivate Collider2D m_CrouchDisableCollider ","scope":"CharacterController2D"},{"signature":"CharacterController2D.k_GroundedRadius","childcount":0,"type":"Field","name":"k_GroundedRadius","token":"const float k_GroundedRadius = .2f;","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_Grounded","childcount":0,"type":"Field","name":"m_Grounded","token":"private bool m_Grounded ","scope":"CharacterController2D"},{"signature":"CharacterController2D.k_CeilingRadius","childcount":0,"type":"Field","name":"k_CeilingRadius","token":"const float k_CeilingRadius = .2f;","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_Rigidbody2D","childcount":0,"type":"Field","name":"m_Rigidbody2D","token":"private Rigidbody2D m_Rigidbody2D ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_FacingRight","childcount":0,"type":"Field","name":"m_FacingRight","token":"private bool m_FacingRight ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_Velocity","childcount":0,"type":"Field","name":"m_Velocity","token":"private Vector3 m_Velocity ","scope":"CharacterController2D"},{"signature":"CharacterController2D.OnLandEvent","childcount":0,"type":"Field","name":"OnLandEvent","token":"[Header(\"Events\")]\n[Space]\npublic UnityEvent OnLandEvent ","scope":"CharacterController2D"},{"signature":"CharacterController2D.BoolEvent","childcount":0,"type":"Class","name":"BoolEvent","token":"[System.Serializable]\npublic class BoolEvent : UnityEvent<bool> ","scope":"CharacterController2D"},{"signature":"CharacterController2D.OnCrouchEvent","childcount":0,"type":"Field","name":"OnCrouchEvent","token":"public BoolEvent OnCrouchEvent ","scope":"CharacterController2D"},{"signature":"CharacterController2D.m_wasCrouching","childcount":0,"type":"Field","name":"m_wasCrouching","token":"private bool m_wasCrouching ","scope":"CharacterController2D"},{"signature":"CharacterController2D.Awake()","childcount":0,"type":"Method","name":"Awake","token":"private void Awake()","scope":"CharacterController2D"},{"signature":"CharacterController2D.FixedUpdate()","childcount":0,"type":"Method","name":"FixedUpdate","token":"private void FixedUpdate()","scope":"CharacterController2D"},{"signature":"CharacterController2D.Move(float, bool, bool)","childcount":0,"type":"Method","name":"Move","token":"public void Move(float move, bool crouch, bool jump)","scope":"CharacterController2D","params":{"move":"float","crouch":"bool","jump":"bool"}},{"signature":"CharacterController2D.Flip()","childcount":0,"type":"Method","name":"Flip","token":"private void Flip()","scope":"CharacterController2D"},{"signature":"EnemyTriggerEvent","childcount":5,"type":"Class","name":"EnemyTriggerEvent","token":"public class EnemyTriggerEvent : MonoBehaviour ","scope":""},{"signature":"EnemyTriggerEvent.loseText","childcount":0,"type":"Field","name":"loseText","token":"public GameObject loseText ","scope":"EnemyTriggerEvent"},{"signature":"EnemyTriggerEvent.cameraObject","childcount":0,"type":"Field","name":"cameraObject","token":"public GameObject cameraObject ","scope":"EnemyTriggerEvent"},{"signature":"EnemyTriggerEvent.Start()","childcount":0,"type":"Method","name":"Start","token":"void Start()","scope":"EnemyTriggerEvent"},{"signature":"EnemyTriggerEvent.Update()","childcount":0,"type":"Method","name":"Update","token":"void Update()","scope":"EnemyTriggerEvent"},{"signature":"EnemyTriggerEvent.OnTriggerEnter2D(Collider2D)","childcount":0,"type":"Method","name":"OnTriggerEnter2D","token":"private void OnTriggerEnter2D(Collider2D collision)","scope":"EnemyTriggerEvent","params":{"collision":"Collider2D"}},{"signature":"Patrol","childcount":4,"type":"Class","name":"Patrol","token":"public class Patrol : MonoBehaviour ","scope":""},{"signature":"Patrol.speed","childcount":0,"type":"Field","name":"speed","token":"public float speed ","scope":"Patrol"},{"signature":"Patrol.movingRight","childcount":0,"type":"Field","name":"movingRight","token":"private bool movingRight ","scope":"Patrol"},{"signature":"Patrol.groundDetection","childcount":0,"type":"Field","name":"groundDetection","token":"public Transform groundDetection ","scope":"Patrol"},{"signature":"Patrol.Update()","childcount":0,"type":"Method","name":"Update","token":"void Update()","scope":"Patrol"},{"signature":"PlayerMovement","childcount":8,"type":"Class","name":"PlayerMovement","token":"public class PlayerMovement : MonoBehaviour ","scope":""},{"signature":"PlayerMovement.controller","childcount":0,"type":"Field","name":"controller","token":"public CharacterController2D controller ","scope":"PlayerMovement"},{"signature":"PlayerMovement.runSpeed","childcount":0,"type":"Field","name":"runSpeed","token":"public float runSpeed ","scope":"PlayerMovement"},{"signature":"PlayerMovement.horizontalMove","childcount":0,"type":"Field","name":"horizontalMove","token":"float horizontalMove ","scope":"PlayerMovement"},{"signature":"PlayerMovement.jump","childcount":0,"type":"Field","name":"jump","token":"bool jump ","scope":"PlayerMovement"},{"signature":"PlayerMovement.crouch","childcount":0,"type":"Field","name":"crouch","token":"bool crouch ","scope":"PlayerMovement"},{"signature":"PlayerMovement.Start()","childcount":0,"type":"Method","name":"Start","token":"void Start()","scope":"PlayerMovement"},{"signature":"PlayerMovement.Update()","childcount":0,"type":"Method","name":"Update","token":"void Update()","scope":"PlayerMovement"},{"signature":"PlayerMovement.FixedUpdate()","childcount":0,"type":"Method","name":"FixedUpdate","token":"private void FixedUpdate()","scope":"PlayerMovement"},{"signature":"PostProcessScript","childcount":5,"type":"Class","name":"PostProcessScript","token":"public class PostProcessScript : MonoBehaviour ","scope":""},{"signature":"PostProcessScript.player","childcount":0,"type":"Field","name":"player","token":"private GameObject player ","scope":"PostProcessScript"},{"signature":"PostProcessScript.cam","childcount":0,"type":"Field","name":"cam","token":"Camera cam ","scope":"PostProcessScript"},{"signature":"PostProcessScript.mat","childcount":0,"type":"Field","name":"mat","token":"public Material mat ","scope":"PostProcessScript"},{"signature":"PostProcessScript.Start()","childcount":0,"type":"Method","name":"Start","token":"void Start()","scope":"PostProcessScript"},{"signature":"PostProcessScript.OnRenderImage(RenderTexture, RenderTexture)","childcount":0,"type":"Method","name":"OnRenderImage","token":"void OnRenderImage(RenderTexture src, RenderTexture dest)","scope":"PostProcessScript","comments":{"summary":"You would not believe what amazing things this method does.","param-src":"A explanation of this really important number","param-dest":"Another explanation"},"params":{"src":"RenderTexture","dest":"RenderTexture"}}
];
const ROOT_MEMBERS = [
{"signature":"CameraMover","childcount":5,"type":"Class","name":"CameraMover","token":"public class CameraMover : MonoBehaviour ","scope":""},{"signature":"CameraTriggerEvent","childcount":5,"type":"Class","name":"CameraTriggerEvent","token":"public class CameraTriggerEvent : MonoBehaviour ","scope":""},{"signature":"CharacterController2D","childcount":22,"type":"Class","name":"CharacterController2D","token":"public class CharacterController2D : MonoBehaviour ","scope":""},{"signature":"EnemyTriggerEvent","childcount":5,"type":"Class","name":"EnemyTriggerEvent","token":"public class EnemyTriggerEvent : MonoBehaviour ","scope":""},{"signature":"Patrol","childcount":4,"type":"Class","name":"Patrol","token":"public class Patrol : MonoBehaviour ","scope":""},{"signature":"PlayerMovement","childcount":8,"type":"Class","name":"PlayerMovement","token":"public class PlayerMovement : MonoBehaviour ","scope":""},{"signature":"PostProcessScript","childcount":5,"type":"Class","name":"PostProcessScript","token":"public class PostProcessScript : MonoBehaviour ","scope":""}
];
const BUILTIN_WORDS_REGEX = /\b(?:__arglist|abstract|as|async|await|base|bool|break|byte|case|catch|char|checked|class|const|continue|decimal|default|delegate|Dictionary|do|double|dynamic|else|enum|event|Exception|extern|false|final|finally|fixed|float|for|foreach|from|from|get|get;|goto|grouby|HashSet|if|in|in|int|interface|internal|is|List|lock|long|nameof|namespace|new|null|object|out|params|partial|private|protected|public|readonly|ref|return|sealed|select|select|set|set;|short|Single|Single32|Single64|stackalloc|static|string|struct|switch|this|throw|true|try|typeof|uint|UInt16|UInt32|UInt64|ulong|unchecked|unsafe|ushort|using|var|virtual|void|volatile|where|where|while)\b/g;