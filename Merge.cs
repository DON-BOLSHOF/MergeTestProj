using System.Collections.Generic;
using UnityEngine;
using Main.ConstValues;
using Main.Checking;
using Main.Map;

namespace Main
{
    public class Merge : MonoBehaviour
    {
        internal static List<GameObject> LineMerge = new List<GameObject>();
        InitializationMap im;

        public GameObject MergedObject; // � ������� ������� �������� ������ ����� ������
        Transform Block1;
        Transform Block2;
        public float Distance;   //  ��� ��������� ������,
        public float MergeSpeed; // �������� ��������� ������������� ���� � �����. �������� ���� �� ���������� ������������� � �����

        bool CanMerge;
        bool MouseReleased;

        private void FixedUpdate() // ������ �������� ����������
        {
            im = FindObjectOfType<InitializationMap>();
            //MoveTowards();
        }

        public void MoveTowards() // �������� ��� ������������� � �������������� ������� � �� ����� ��������� � OnCollisionStay2D?
        {
            if (CanMerge)
            {
                transform.position = Vector2.MoveTowards(Block1.position, Block2.position, MergeSpeed);
                GameObject O = Instantiate(MergedObject, transform.position, Quaternion.identity);// ������� ����� ������
                O.GetComponent<ObjectData>().currentPosition = Block1.gameObject.GetComponent<ObjectData>().currentPosition;
                gameObject.GetComponent<ObjectData>().currentPosition.GetComponent<GridData>().isEmpty = true;
                Block2.gameObject.GetComponent<ObjectData>().currentPosition.GetComponent<GridData>().isEmpty = true;
                Destroy(Block2.gameObject);// ���������� ��, ���
                Destroy(gameObject);       //    �����������
            }
        }

        private void OnCollisionStay2D(Collision2D collision) // ���� ���� ������ �������� �������
        {
            if (MouseReleased) // ���� ���� ���������
            {
                if (collision.gameObject.CompareTag(Tags.MergeBlock)) // ���� ������ �������� �������, ������� ����� �����������
                {
                    if (collision.gameObject.GetComponent<SpriteRenderer>().sprite == GetComponent<SpriteRenderer>().sprite)
                    {
                        Block1 = transform;
                        Block2 = collision.transform;
                        CanMerge = true;
                    }
                }
                MouseReleased = false; // ��� ������ ���������, ����� �� ����������, ���� ����� � ������� "�� ���������"
            }
        }

        void OnMouseUp()
        {
            MouseReleased = true;
            CellEditing.ChangeObj(gameObject);
            im.FindFriendsCell(Input.mousePosition);
        }
    }
}