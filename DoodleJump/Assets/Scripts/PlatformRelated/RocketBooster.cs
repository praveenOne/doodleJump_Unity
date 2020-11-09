using System.Collections;
using UnityEngine;

namespace praveen.one
{
    public class RocketBooster : Platform
    {
        #region member_variables
        GameObject m_JetPack;
        [SerializeField] float m_JetPackLifeTime;
        #endregion


        public override void Start()
        {
            base.Start();
            m_JetPack = transform.GetChild(0).gameObject;
        }

        public override void Update() { }

        public override int OnStep(GameObject player)
        {
            AttachJetpack(player);
            return base.OnStep(player);
        }

        private void AttachJetpack(GameObject player)
        {
            m_JetPack.transform.parent = player.transform;
            m_JetPack.transform.localPosition = new Vector3(0, 0, -0.5f);
            StartCoroutine(JetpakFly(player));
        }

        IEnumerator JetpakFly(GameObject player)
        {
            float normalizedTime = 0;
            while (normalizedTime <= 1f)
            {
                normalizedTime += Time.deltaTime / m_JetPackLifeTime;

                yield return null;
                player.transform.Translate(Vector3.up * 0.4f);
                m_JetPack.transform.localPosition = new Vector3(0, 0, -0.5f);
            }

            m_JetPack.transform.parent = null;
            m_JetPack.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
            StartCoroutine(Reset());
        }

        IEnumerator Reset()
        {
            yield return new WaitForSeconds(3);
            m_JetPack.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            m_JetPack.transform.parent = transform;
            m_JetPack.transform.localPosition = new Vector3(0, 0.6f, 0);
            Recycle();

        }
    }
}
