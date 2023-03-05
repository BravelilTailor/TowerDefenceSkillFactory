using UnityEngine;

/// <summary>
/// Базовый класс интерактивных игровых обьектов на сцене
/// </summary>

public abstract class Entity : MonoBehaviour
{
    /// <summary>
    /// Название обьекта пользователя
    /// </summary>
    
    [SerializeField]
    private string m_nickname;
    public string Nickname => m_nickname;
}
