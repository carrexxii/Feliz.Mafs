namespace Feliz.Mafs

open System

[<AutoOpen>]
module Maths =
    type Angle =
        | Degrees of float
        | Radians of float
        static member toRad = function
            | Degrees deg -> deg / (2.0*Math.PI)
            | Radians rad -> rad
        static member ( * ) (a: Angle, s: float) =
            match a with
            | Degrees a -> Degrees (a * s)
            | Radians a -> Radians (a * s)
        static member ( * ) (s: float, a: Angle) = a * s

    type Vec2 (x, y) =
        new (arr: float array)  = Vec2 (arr[0], arr[1])
        new (pt : MovablePoint) = Vec2 (pt.x, pt.y)
        member this.x: float = x
        member this.y: float = y

        member this.array = [| this.x; this.y |]

        member this.dist (v: Vec2)            = Verbatim.vec.dist       (this.array, v.array)
        member this.mag                       = Verbatim.vec.mag        this.array
        member this.lerp (v: Vec2) (t: float) = Verbatim.vec.lerp       (this.array, v.array, t) |> Vec2
        member this.midpoint (v: Vec2)        = Verbatim.vec.midpoint   (this.array, v.array)    |> Vec2
        member this.normal                    = Verbatim.vec.normal     this.array               |> Vec2
        member this.squareDist (v: Vec2)      = Verbatim.vec.squareDist (this.array, v.array)    |> Vec2
        member this.withMag (s: float)        = Verbatim.vec.withMag    (this.array, s)          |> Vec2
        member this.rotate angle =
            match angle with
            | Degrees deg -> (this.array, deg / (2.0 * System.Math.PI))
            | Radians rad -> (this.array, rad)
            |> Verbatim.vec.rotate |> Vec2
        member this.rotateAbout (v: Vec2) angle =
            match angle with
            | Degrees deg -> (this.array, v.array, deg / (2.0 * System.Math.PI))
            | Radians rad -> (this.array, v.array, rad)
            |> Verbatim.vec.rotateAbout |> Vec2

        static member ( + ) (v: Vec2 , w: Vec2)  = Verbatim.vec.add   (v.array, w.array) |> Vec2
        static member ( - ) (v: Vec2 , w: Vec2)  = Verbatim.vec.sub   (v.array, w.array) |> Vec2
        static member ( * ) (v: Vec2 , s: float) = Verbatim.vec.scale (v.array, s)       |> Vec2
        static member ( * ) (s: float, v: Vec2)  = Verbatim.vec.scale (v.array, s)       |> Vec2
        static member ( * ) (v: Vec2 , w: Vec2)  = Verbatim.vec.dot   (v.array, w.array)

    [<AutoOpen>]
    type Transform =
        static member translate (vec: Vec2) elems =
            Verbatim.Transform (elems, translate = vec.array)

    let vec x y = Vec2 (x, y)

    type Constrain =
        | Constrain of (Vec2 -> Vec2)
        | Horizontal
        | Vertical
        | None
