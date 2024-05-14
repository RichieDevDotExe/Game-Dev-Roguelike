using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBarManager : MonoBehaviour
{
    private Slider healthBarSlider;
    [SerializeField]private Enemy enemy;
    private float healthPercent;
    private Canvas canvas;
    private Camera camera;

    void Start()
    {
        enemy = GetComponent<Enemy>();
        canvas = transform.Find("Canvas").GetComponent<Canvas>();
        healthBarSlider = canvas.transform.Find("HealthBar").gameObject.GetComponent<Slider>();
        camera = GameObject.Find("MainCamera").GetComponent<Camera>();
    }

    //updates canvas attached to enemy
    void Update()
    {
        healthPercent = enemy.EntityHealth / enemy.EntityMaxHealth;
        healthBarSlider.value = healthPercent;
        canvas.transform.LookAt(camera.transform.position);
    }

}
