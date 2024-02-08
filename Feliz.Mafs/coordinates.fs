namespace Feliz.Mafs

open Fable.Core.JsInterop
open Feliz

[<AutoOpen>]
module Coordinates =
    module Cartesian =
        type Props =
            { xAxis : Axis option
              yAxis : Axis option
              subDiv: float }
            static member Default =
                { xAxis  = Some Axis.Default
                  yAxis  = Some Axis.Default
                  subDiv = 1 }

        let create () = Props.Default
        let xAxis  xAxis  props = { props with xAxis  = xAxis  }
        let yAxis  yAxis  props = { props with yAxis  = yAxis  }
        let subDiv subDiv props = { props with subDiv = subDiv }

        [<ReactComponent>]
        let render props =
            let render x y =
                Verbatim.Coordinates.Cartesian (xAxis = x, yAxis = y, subdivisions = props.subDiv)
            match props.xAxis with
            | None -> match props.yAxis with
                      | None       -> render !^false !^false
                      | Some yAxis -> render !^false !^yAxis
            | Some xAxis -> match props.yAxis with
                            | None       -> render !^xAxis !^false
                            | Some yAxis -> render !^xAxis !^yAxis

    module Polar =
        type Props =
            { xAxis : Axis Option
              yAxis : Axis Option
              lines : float
              subDiv: float }
            static member Default =
                { xAxis  = Some Axis.Default
                  yAxis  = Some Axis.Default
                  lines  = 1
                  subDiv = 1 }

        let create () = Props.Default
        let xAxis  xAxis  props = { props with xAxis  = xAxis  }: Props
        let yAxis  yAxis  props = { props with yAxis  = yAxis  }: Props
        let lines  lines  props = { props with lines  = lines  }: Props
        let subDiv subDiv props = { props with subDiv = subDiv }: Props

        [<ReactComponent>]
        let render props =
            let render x y =
                Verbatim.Coordinates.Polar (xAxis = x, yAxis = y, subdivisions = props.subDiv, lines = props.lines)
            match props.xAxis with
            | None -> match props.yAxis with
                      | None       -> render !^false !^false
                      | Some yAxis -> render !^false !^yAxis
            | Some xAxis -> match props.yAxis with
                            | None       -> render !^xAxis !^false
                            | Some yAxis -> render !^xAxis !^yAxis
