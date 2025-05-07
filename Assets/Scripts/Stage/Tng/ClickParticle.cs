using UnityEngine;

public class ClickParticle : MonoBehaviour
{
    public ParticleSystem clickParticleSystem;
    public float speed = 5f;
    public int particleCount = 8;
    private float size = 0.2f;
    private Color color;

    private void Start()
    {
        EmitInCircle();
    }
    public void Init(float size, Color color)
    {
        this.size = size;
        this.color = color;
    }
    public void EmitInCircle()
    {
        float angleStep = 360f / particleCount;

        for (int i = 0; i < particleCount; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 direction = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);

            ParticleSystem.EmitParams emitParams = new ParticleSystem.EmitParams();
            emitParams.velocity = direction * speed;
            emitParams.startSize = size;
            emitParams.startColor = color;

            clickParticleSystem.Emit(emitParams, 1);
        }
    }
}
