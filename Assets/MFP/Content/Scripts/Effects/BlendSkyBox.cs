using UnityEngine;

namespace DestroyIt
{
    public class BlendSkyBox : MonoBehaviour
    {
        public bool reverseBlend = false;
        public float delaySeconds = 0f;

        private float currentValue;
        private bool skipFrame;

        private void FixedUpdate()
        {
            if (!RenderSettings.skybox.HasProperty("_Blend"))
            {
                Destroy(this);
                return;
            }

            if (!skipFrame)
            {
                currentValue = RenderSettings.skybox.GetFloat("_Blend");
                if (currentValue > 1f) currentValue = 1f;
                if (currentValue < 0f) currentValue = 0f;
                if (!reverseBlend && currentValue >= 1f)
                {
                    Destroy(this);
                    return;
                }
                if (reverseBlend && currentValue <= 0f)
                {
                    Destroy(this);
                    return;
                }

                if (!reverseBlend)
                    currentValue += 0.01f;
                else
                    currentValue -= 0.01f;

                RenderSettings.skybox.SetFloat("_Blend", currentValue);

                Color fogColor = RenderSettings.fogColor;

                if (!reverseBlend && fogColor.r > .25f)
                    RenderSettings.fogColor = new Color(fogColor.r - .005f, fogColor.g - .007f, fogColor.b - .007f, 1f);
                else if (fogColor.r < .53f)
                    RenderSettings.fogColor = new Color(fogColor.r + .005f, fogColor.g + .007f, fogColor.b + .007f, 1f);
            }
            skipFrame = !skipFrame;
        }
    }
}