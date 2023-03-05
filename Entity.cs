using UnityEngine;

/// <summary>
/// ������� ����� ������������� ������� �������� �� �����
/// </summary>

public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// �������� ������� ������������
    /// </summary>
    
    [SerializeField]
    private string m_nickname;
    public string Nickname => m_nickname;
}
