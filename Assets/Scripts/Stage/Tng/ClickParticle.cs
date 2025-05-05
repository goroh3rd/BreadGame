using UnityEngine;

public class ClickParticle : MonoBehaviour
{
    public ParticleSystem clickParticleSystem;
    public float speed = 5f;
    public int particleCount = 8;

    private void Start()
    {
        EmitInCircle();
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
            emitParams.startSize = 0.2f;

            clickParticleSystem.Emit(emitParams, 1);
        }
    }
}
