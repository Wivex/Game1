using UnityEngine;
using TMPro;

public class EnemyPanelDrawer : UnitPanelDrawer
{
    [Header("Enemy")] public Enemy enemy;
    public TextMeshProUGUI enemyName;

    public void Init(Enemy enemy)
    {
        this.enemy = enemy;
        unit = enemy;
        unitImage.sprite = enemy.data.sprite;
        enemyName.text = enemy.data.name;
    }

    protected override void Update()
    {
        if (!canvas.enabled) return;

        base.Update();
    }
}