namespace MapImporter.Enums
{
    /// <summary>
    /// The different render orders possible for drawing the maps.
    /// </summary>
    public enum RenderOrder
    {
        /// <summary>
        /// The Tiled map should be rendered right and down.
        /// </summary>
        RightDown,
        /// <summary>
        /// The Tiled map should be rendered right and up.
        /// </summary>
        RightUp,
        /// <summary>
        /// The Tiled map should be rendered left and down.
        /// </summary>
        LeftDown,
        /// <summary>
        /// The Tiled map should be rendered left and up.
        /// </summary>
        LeftUp
    }
}
