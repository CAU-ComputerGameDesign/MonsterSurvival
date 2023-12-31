using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using Color = UnityEngine.Color;

namespace MimicSpace
{
    public class Tentacle : MonoBehaviour
    {
        Mimic myMimic;
        public bool isDeployed = false;
        public Vector3 attackPoint;
        public float maxLegDistance;
        public int legResolution;
        //public GameObject legObject;
        public LineRenderer legLine;
        public int handlesCount = 8; // 8 (7 legs + 1 finalfoot)

        public float legMinHeight;
        public float legMaxHeight;
        float legHeight;
        public Vector3[] handles;
        public float handleOffsetMinRadius;
        public float handleOffsetMaxRadius;
        public Vector3[] handleOffsets;
        public float finalFootDistance;

        public float growCoef;
        public float growTarget = 1;

        [Range(0, 1f)]
        public float progression;

        public bool canAttack = true;
        public bool isShrink = false;

        [Header("Rotation")]
        public float rotationSpeed;
        public float minRotSpeed;
        public float maxRotSpeed;
        float rotationSign = 1;
        public float oscillationSpeed;
        public float minOscillationSpeed;
        public float maxOscillationSpeed;
        float oscillationProgress;

        private Transform center;

        public Color myColor;

        public AnimationCurve animationCurve;
        private float deltaTime;
        public float hitRate = 0.5f;

        public float damage = 1.5f;
        
        public SphereCollider collider;
        public void Initialize(Vector3 attackPoint, int legResolution, float maxLegDistance, float growCoef, Mimic myMimic, float lifeTime , Transform center)
        {
            this.attackPoint = attackPoint;
            this.legResolution = legResolution;
            this.maxLegDistance = maxLegDistance;
            this.growCoef = growCoef;
            this.myMimic = myMimic;

            this.legLine = GetComponent<LineRenderer>();
            collider = GetComponent<SphereCollider>();
            legLine.material.color = myColor;
            handles = new Vector3[handlesCount];

            // We initialize a bunch of random offsets for many aspects of the legs so every leg part is unique
            // This will make the leg look more organic
            handleOffsets = new Vector3[6];
            handleOffsets[0] = Random.onUnitSphere * Random.Range(handleOffsetMinRadius, handleOffsetMaxRadius);
            handleOffsets[1] = Random.onUnitSphere * Random.Range(handleOffsetMinRadius, handleOffsetMaxRadius);
            handleOffsets[2] = Random.onUnitSphere * Random.Range(handleOffsetMinRadius, handleOffsetMaxRadius);
            handleOffsets[3] = Random.onUnitSphere * Random.Range(handleOffsetMinRadius, handleOffsetMaxRadius);
            handleOffsets[4] = Random.onUnitSphere * Random.Range(handleOffsetMinRadius, handleOffsetMaxRadius);
            handleOffsets[5] = Random.onUnitSphere * Random.Range(handleOffsetMinRadius, handleOffsetMaxRadius);

            // each leg part have the same foot position, butto make it look like "toes" the last handle (handles[7])
            // is a bit offset for every leg part
            handles[7] = this.attackPoint;
            handles[7].y += 0.5f;

            legHeight = Random.Range(legMinHeight, legMaxHeight);
            rotationSpeed = Random.Range(minRotSpeed, maxRotSpeed); // * (Random.Range(0f, 1f) > 0.5f ? -1 : 1);
            rotationSign = 1;//(Random.Range(0f, 1f) > 0.5f ? -1 : 1);
            oscillationSpeed = Random.Range(minOscillationSpeed, maxOscillationSpeed);
            oscillationProgress = 0;

            myMimic.tentacleCount++;
            growTarget = 1;

            this.center = center;

            isDeployed = false;
            Sethandles();
        }


        public void Attack(Vector3 target, float damage)
        {
            this.damage = damage;
            collider.enabled = true;
            canAttack = false;
            isShrink = false;
            attackPoint = target;
            handles[7] = attackPoint;
            handles[7].y += 0.5f;
            Sethandles();
            deltaTime = 0;
        }

        private void Update()
        {
            deltaTime += Time.deltaTime;
            legLine.widthMultiplier = myMimic.legSize;
            
            transform.localPosition = center.localPosition;
            
            progression = animationCurve.Evaluate(deltaTime / hitRate);

            if (progression >= 0.8f)
            {
                isShrink = true;
            }

            if (progression >= 1.2f)
            {
                collider.enabled = false;
            }

            if (progression < 0.05f)
            {
                if (isShrink) canAttack = true;
            }
            // We update the handle position defining the spline
            Sethandles();

            // Then sample the spline and assign the values to the line renderer
            Vector3[] points = GetSamplePoints((Vector3[])handles.Clone(), legResolution, progression);
            legLine.positionCount = points.Length;
            legLine.SetPositions(points);
        }

        void Sethandles()
        {
            // Start handle at body position
            handles[0] = transform.position;

            // The foot position is moved upward,
            // in combination with the Handles[7] offset it will look like an "ankle"
            handles[6] = attackPoint;

            // we take a point 40% along the leg and raise it to make the highest part of the leg
            handles[2] = Vector3.Lerp(handles[0], handles[6], 0.4f);

            // then we interpolate the rest of the handles
            handles[1] = Vector3.Lerp(handles[0], handles[2], 0.5f);
            handles[3] = Vector3.Lerp(handles[2], handles[6], 0.25f);
            handles[4] = Vector3.Lerp(handles[2], handles[6], 0.5f);
            handles[5] = Vector3.Lerp(handles[2], handles[6], 0.75f);

            // we rotate the handles offsets based on the leg axis to make them look alive
            RotateHandleOffset();

            // and we apply the offsets to the handle position
            handles[1] += handleOffsets[0];
            handles[2] += handleOffsets[1];
            handles[3] += handleOffsets[2];
            handles[4] += handleOffsets[3] / 2f;
            handles[5] += handleOffsets[4] / 4f;
        }

        void RotateHandleOffset()
        {
            oscillationProgress += Time.deltaTime * oscillationSpeed;
            if (oscillationProgress >= 360f)
                oscillationProgress -= 360f;

            float newAngle = rotationSpeed * Time.deltaTime * Mathf.Cos(oscillationProgress * Mathf.Deg2Rad) + 1f;

            Vector3 axisRotation;
            for (int i = 1; i < 6; i++)
            {
                axisRotation = (handles[i + 1] - handles[i - 1]) / 2f;
                handleOffsets[i - 1] = Quaternion.AngleAxis(newAngle, rotationSign * axisRotation) * handleOffsets[i - 1];
            }

        }

        Vector3[] GetSamplePoints(Vector3[] curveHandles, int resolution, float t)
        {
            List<Vector3> segmentPos = new List<Vector3>();
            float segmentLength = 1f / (float)resolution;

            for (float _t = 0; _t <= t; _t += segmentLength)
                segmentPos.Add(GetPointOnCurve((Vector3[])curveHandles.Clone(), _t));
            segmentPos.Add(GetPointOnCurve(curveHandles, t));

            collider.center = transform.InverseTransformPoint(segmentPos[segmentPos.Count - 1]);
            return segmentPos.ToArray();
        }

        Vector3 GetPointOnCurve(Vector3[] curveHandles, float t)
        {
            int currentPoints = curveHandles.Length;

            while (currentPoints > 1)
            {
                for (int i = 0; i < currentPoints - 1; i++)
                    curveHandles[i] = Vector3.Lerp(curveHandles[i], curveHandles[i + 1], t);
                currentPoints--;
            }
            return curveHandles[0];
        }
        
        
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Monster"))
            {
                AnimalsController controller = other.GetComponent<AnimalsController>();
                controller.Hit(damage);
            }
            else if (!other.CompareTag("Player")) ;
        }
    }
}