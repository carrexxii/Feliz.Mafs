namespace Feliz.Mafs

open Feliz

[<AutoOpen>]
module Geometry =
    [<ReactComponent>]
    let Point (vec: Vec2) =
        Verbatim.Point (vec[0], vec[1])

    let movablePoint initial =
        Verbatim.useMovablePoint initial

    let constrainedPoint initial constrain =
        Verbatim.useMovablePoint (initial, constrain = constrain)

    [<ReactComponent>]
    let Circle center radius =
        Verbatim.Circle (center = center, radius = radius)

    [<ReactComponent>]
    let Polygon points =
        Verbatim.Polygon (points = points)
