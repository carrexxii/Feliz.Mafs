namespace Feliz.Mafs

open Fable.Core
open Fable.Core.JsInterop
open Feliz

[<AutoOpen>]
module Coordinates =
    type CartesianProps =
        { xAxis : U2<bool, Axis>
          yAxis : U2<bool, Axis>
          subDiv: float }
        static member Default =
            { xAxis  = !^Axis.Default
              yAxis  = !^Axis.Default
              subDiv = 1 }

    type PolarProps =
        { xAxis : U2<bool, Axis>
          yAxis : U2<bool, Axis>
          lines : float
          subDiv: float }
        static member Default =
            { xAxis  = !^Axis.Default
              yAxis  = !^Axis.Default
              lines  = 1
              subDiv = 1 }

    [<ReactComponent>]
    let Cartesian (props: CartesianProps) = 
        Verbatim.Coordinates.Cartesian (xAxis = props.xAxis,
                                        yAxis = props.yAxis,
                                        subdivisions = props.subDiv)
    let CartesianDefault = Cartesian CartesianProps.Default

    [<ReactComponent>]
    let Polar (props: PolarProps) = 
        Verbatim.Coordinates.Polar (xAxis = props.xAxis,
                                    yAxis = props.yAxis,
                                    lines = props.lines,
                                    subdivisions = props.subDiv)
    let PolarDefault = Polar PolarProps.Default
