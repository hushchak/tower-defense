using UnityEngine;

public class TowerPlacementSounds : MonoBehaviour
{
    [SerializeField] private TowerSelector towerSelector;

    [Header("Sounds")]
    [SerializeField] private Sound towerPlacedSound;
    [SerializeField] private Sound towerCantBePlacedSound;
    [SerializeField] private Sound towerCardSelectedSound;
    [SerializeField] private Sound towerCardSelectionDeniedSound;

    private void OnEnable()
    {
        towerSelector.TowerPlaced += PlayTowerPlacedSound;
        towerSelector.TowerCantBePlaced += PlayTowerCantBePlacedSound;
        towerSelector.TowerCardSelected += PlayTowerCardSelectedSound;
        towerSelector.TowerCardSelectionDenied += PlayTowerCardSelectionDeniedSound;
    }

    private void OnDisable()
    {
        towerSelector.TowerPlaced -= PlayTowerPlacedSound;
        towerSelector.TowerCantBePlaced -= PlayTowerCantBePlacedSound;
        towerSelector.TowerCardSelected -= PlayTowerCardSelectedSound;
        towerSelector.TowerCardSelectionDenied -= PlayTowerCardSelectionDeniedSound;
    }

    private void PlayTowerPlacedSound() => Audio.Play(towerPlacedSound);
    private void PlayTowerCantBePlacedSound() => Audio.Play(towerCantBePlacedSound);
    private void PlayTowerCardSelectedSound() => Audio.Play(towerCardSelectedSound);
    private void PlayTowerCardSelectionDeniedSound() => Audio.Play(towerCardSelectionDeniedSound);
}
