using System;
using System.Collections.Generic;

namespace UnityEngine.PostProcessing {
    public sealed class MaterialFactory : IDisposable {
        private Dictionary<string, Material> m_Materials;

        public MaterialFactory() {
            m_Materials = new Dictionary<string, Material>();
        }

        public Material Get(string shaderName) {

            if (!m_Materials.TryGetValue(shaderName, out var material)) {
                var shader = Shader.Find(shaderName);

                if (shader == null) {
                    throw new ArgumentException(string.Format("Shader not found ({0})", shaderName));
                }

                material = new Material(shader) {
                    name = string.Format("PostFX - {0}", shaderName.Substring(shaderName.LastIndexOf("/") + 1)),
                    hideFlags = HideFlags.DontSave
                };

                m_Materials.Add(shaderName, material);
            }

            return material;
        }

        public void Dispose() {
            var enumerator = m_Materials.GetEnumerator();
            while (enumerator.MoveNext()) {
                var material = enumerator.Current.Value;
                GraphicsUtils.Destroy(material);
            }

            m_Materials.Clear();
        }
    }
}
