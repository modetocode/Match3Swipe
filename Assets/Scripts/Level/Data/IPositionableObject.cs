using UnityEngine;

namespace Modetocode.Swiper.Level.Data {

    /// <summary>
    /// Represents an object that is positionable in space.
    /// </summary>
    public interface IPositionableObject {
        Vector2 Position { get; set; }
    }
}
