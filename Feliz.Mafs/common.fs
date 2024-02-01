namespace Feliz.Mafs

open Fable.Core
open Fable.Core.JsInterop
open Feliz

[<AutoOpen>]
module Common =
    type Matrix = float array
    type IMatrixBuilder =
        { mult     : Matrix        -> Matrix
          translate: float * float -> Matrix
          rotate   : float         -> Matrix
          scale    : float * float -> Matrix
          shear    : float * float -> Matrix
          get      : unit          -> Matrix }

    type Color = string

    type ITheme =
        abstract foreground: Color
        abstract background: Color
        abstract red       : Color
        abstract orange    : Color
        abstract green     : Color
        abstract blue      : Color
        abstract indigo    : Color
        abstract violet    : Color
        abstract pink      : Color
        abstract yellow    : Color
    let Theme: ITheme = import "Theme" "mafs"

    [<StringEnum>] type Auto    = Auto
    [<StringEnum>] type Contain = Contain

    type SVGProps = obj

    [<StringEnum>]
    type StrokeStyle =
        | Solid
        | Dashed

    module Labels =
        [<Import("labelPi", "mafs")>]
        let Pi = jsNative<int -> string>

    [<StringEnum>]
    type Direction =
        | [<CompiledName("n")>] North | [<CompiledName("ne")>] NorthEast
        | [<CompiledName("e")>] East  | [<CompiledName("se")>] SouthEast
        | [<CompiledName("s")>] South | [<CompiledName("sw")>] SouthWest
        | [<CompiledName("w")>] West  | [<CompiledName("nw")>] NorthWest

    type MovablePoint =
        { element : ReactElement
          point   : float array
          setPoint: float array -> unit
          x       : float
          y       : float }

    [<StringEnum>]
    type LineStyle =
        | Solid
        | Dashed

    [<StringEnum>]
    type Inequality =
        | [<CompiledName(">" )>] GT
        | [<CompiledName(">=")>] GTEQ
        | [<CompiledName("<" )>] LT
        | [<CompiledName("<=")>] LTEQ

    type KatexOptions = obj

    type XYAxis =
        | XAxis
        | YAxis

    type Axis =
        { lines : float
          labels: int -> string }
        static member Default =
            { lines  = 1.0
              labels = fun x -> $"{x}" }

    type ViewBox =
        { x      : float array
          y      : float array
          padding: float }
        static member Default =
            { x       = [| -3; 3 |]
              y       = [| -3; 3 |]
              padding = 0.5 }
