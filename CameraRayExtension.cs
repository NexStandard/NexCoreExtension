using Stride.Core.Mathematics;
using Stride.Engine;
using Stride.Physics;

namespace NexCoreExtension;

public static class CameraRayExtension
{
    public static HitResult RayCastMouse(this CameraComponent Camera, ScriptComponent component, CollisionFilterGroups groups = CollisionFilterGroups.DefaultFilter)
    {
        return Camera.RayCast(component, component.Input.MousePosition, groups);
    }
    public static HitResult RayCast(this CameraComponent Camera, ScriptComponent component, Vector2 screenPos, CollisionFilterGroups groups = CollisionFilterGroups.DefaultFilter)
    {
        Matrix invViewProj = Matrix.Invert(Camera.ViewProjectionMatrix);
        // Reconstruct the projection-space position in the (-1, +1) range.
        //    Don't forget that Y is down in screen coordinates, but up in projection space
        Vector3 sPos;
        sPos.X = screenPos.X * 2f - 1f;
        sPos.Y = 1f - screenPos.Y * 2f;

        // Compute the near (start) point for the raycast
        // It's assumed to have the same projection space (x,y) coordinates and z = 0 (lying on the near plane)
        // We need to unproject it to world space
        sPos.Z = 0f;
        var vectorNear = Vector3.Transform(sPos, invViewProj);
        vectorNear /= vectorNear.W;

        // Compute the far (end) point for the raycast
        // It's assumed to have the same projection space (x,y) coordinates and z = 1 (lying on the far plane)
        // We need to unproject it to world space
        sPos.Z = 1f;
        var vectorFar = Vector3.Transform(sPos, invViewProj);
        vectorFar /= vectorFar.W;
        
        // Raycast from the point on the near plane to the point on the far plane and get the collision result
        return component.GetSimulation().Raycast(vectorNear.XYZ(), vectorFar.XYZ(), groups);
    }
}
