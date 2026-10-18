using UnityEngine;

public class JoinPromptScr : MonoBehaviour
{
    private RectTransform _rect;
    [SerializeField]private float scaleAmount;
    private float _scaleTo;

    [Range(0, 1)]
    [SerializeField]private float slowDownPoint;
    [SerializeField]private float slowDownAmount;

    private bool _scale;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _rect = gameObject.GetComponent<RectTransform>();
        _scaleTo = _rect.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        if (_rect.localScale.x < _scaleTo && _scale)
        {
            var amount = scaleAmount;
            if (_rect.localScale.x >= slowDownPoint)
            {
                amount =  slowDownAmount;
            }
            Vector2 size = _rect.localScale;
            size.x += amount * Time.deltaTime;
            size.y += amount * Time.deltaTime;
            _rect.localScale = size;
        } else if (_rect.localScale.x > _scaleTo)
        {
            _rect.localScale = new Vector2(_scaleTo, _scaleTo);
        }
    }

    public void Appear()
    {
        _rect.localScale = new Vector2(0.2f, 0.2f);
        gameObject.SetActive(true);
        _scale = true;
    }
}
