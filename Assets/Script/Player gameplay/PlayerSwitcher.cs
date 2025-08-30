using UnityEngine;
using UnityEngine.InputSystem;
using Unity.Cinemachine;

public class PlayerSwitcher : MonoBehaviour
{
    [Header("Players")]
    public Player[] players;
    
    [Header("Camera")]
    public CinemachineCamera cinemachineCamera;
    
    [Header("Visual Feedback")]
    public Color activePlayerColor = Color.red;
    public Color inactivePlayerColor = Color.gray;
    
    private int currentPlayerIndex = 0;
    private Player currentActivePlayer;
    
    void Start()
    {
        if (players.Length == 0)
        {
            Debug.LogError("No players assigned to PlayerSwitcher!");
            return;
        }
        
        // Initialize the first player as active
        SwitchToPlayer(0);
    }
    
    void Update()
    {
        HandleSwitchInput();
    }
    
    private void HandleSwitchInput()
    {
        if (Keyboard.current.spaceKey.wasPressedThisFrame)
        {
            SwitchToNextPlayer();
        }
    }
    
    private void SwitchToNextPlayer()
    {
        if (players.Length <= 1) return;
        
        // Calculate next player index (loop back to 0 if at the end)
        int nextPlayerIndex = (currentPlayerIndex + 1) % players.Length;
        SwitchToPlayer(nextPlayerIndex);
    }
    
    private void SwitchToPlayer(int playerIndex)
    {
        if (playerIndex < 0 || playerIndex >= players.Length) return;
        
        // Deactivate current player
        if (currentActivePlayer != null)
        {
            currentActivePlayer.SetActive(false);
            UpdatePlayerVisuals(currentActivePlayer, false);
        }
        
        // Activate new player
        currentPlayerIndex = playerIndex;
        currentActivePlayer = players[currentPlayerIndex];
        currentActivePlayer.SetActive(true);
        UpdatePlayerVisuals(currentActivePlayer, true);
        
        // Update camera to follow new player
        UpdateCameraTarget(currentActivePlayer.transform);
        
        Debug.Log($"Switched to {currentActivePlayer.name}");
    }
    
    private void UpdateCameraTarget(Transform newTarget)
    {
        if (cinemachineCamera != null)
        {
            cinemachineCamera.Target.TrackingTarget = newTarget;
        }
    }
    
    private void UpdatePlayerVisuals(Player player, bool isActive)
    {
        SpriteRenderer spriteRenderer = player.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            spriteRenderer.color = isActive ? activePlayerColor : inactivePlayerColor;
        }
    }
    
    public Player GetActivePlayer()
    {
        return currentActivePlayer;
    }
    
    public bool IsPlayerActive(Player player)
    {
        return currentActivePlayer == player;
    }
}
