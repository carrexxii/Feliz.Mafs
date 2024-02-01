namespace Feliz.Mafs

open Browser.Types
open Fable.Core
open Fable.Core.JsInterop
open Feliz

[<AutoOpen>]
module Mafs =
    type MafsProps =
        { width  : U2<float, Auto>
          height : float
          pan    : bool
          zoom   : U2<bool, {| min: float; max: float |}>
          viewBox: ViewBox
          preserveAspectRatio: U2<bool, Contain>
          ssr    : bool
          onClick: float array -> MouseEvent -> unit }
        static member Default =
            { width   = !^Auto
              height  = 500
              pan     = true
              zoom    = !^false
              viewBox = ViewBox.Default
              onClick = fun _ _ -> ()
              ssr     = false
              preserveAspectRatio = !^Contain }

    let Mafs props (children: ReactElement list) =
        Verbatim.Mafs (children = (prop.key 1)::[prop.children children], width = props.width, height = props.height,
                       pan = props.pan, zoom = props.zoom, viewBox = props.viewBox,
                       preserveAspectRatio = props.preserveAspectRatio, ssr = props.ssr, onClick = props.onClick)
