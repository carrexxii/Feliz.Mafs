module Examples

open Fable.Core.JsInterop
open Feliz

open SharpMafs

[<ReactComponent>]
let Heading text =
    Html.h1 [
        prop.className "head"
        prop.text (string text)
    ]

[<ReactComponent>]
let SubHeading text =
    Html.h2 [
        prop.className "subhead"
        prop.text (string text)
    ]

[<ReactComponent>]
let CodeBlock (text: string) =
    Html.span [
        prop.className "text-slate-200 bg-black"
        prop.text text
    ]

[<ReactComponent>]
let GetStarted () =
    Html.div [
        Heading "Get Started Examples"

        SubHeading "Drawing a coordinate plane"
        Mafs [
            prop.key 1
            prop.children [
                Cartesian ()
            ]
        ]
        CodeBlock
            """ import { Mafs, Coordinates } from "mafs"

                function HelloFx() {
                    return (
                        <Mafs>
                            <Coordinates.Cartesian />
                        </Mafs>
                    )
                }
            """

        Mafs [
            prop.key 1
            prop.children [
                Cartesian (subdivisions = 4)
            ]
        ]
        CodeBlock
            """ import { Mafs, Coordinates } from "mafs"

                function HelloFx() {
                    return (
                        <Mafs>
                            <Coordinates.Cartesian subdivisions={4} />
                        </Mafs>
                    )
                }
            """

        SubHeading "Plotting a function"
        Mafs [
            prop.key 1
            prop.children [
                Cartesian (subdivisions = 4)
                Plot.OfX {| y = fun x -> sin x |}
            ]
        ]
        CodeBlock
            """ import { Mafs, Coordinates, Plot } from "mafs"

                function HelloFx() {
                    return (
                        <Mafs>
                            <Coordinates.Cartesian subdivisions={4} />
                            <Plot.OfX y={(x) => Math.sin(x)} />
                        </Mafs>
                    )
                }
            """

        Mafs ([
            prop.key 1
            prop.children [
                Cartesian (subdivisions = 4,
                           xAxis = { lines  = Mafs.pi
                                     labels = Pi })
                Plot.OfX {| y = fun x -> sin x |}
            ]
        ], viewBox = { ViewBox.Default with
                         x = vec2 -10 10
                         y = vec2 -2  2 }
         , preserveAspectRatio = !^false)
        CodeBlock
            """ import { Mafs, Coordinates, Plot, labelPi } from "mafs"

                function HelloFx() {
                    return (
                        <Mafs
                            viewBox={{ x: [-10, 10], y: [-2, 2] }}
                            preserveAspectRatio={false}
                        >
                            <Coordinates.Cartesian
                                subdivisions={4}
                                xAxis={{ lines: Math.PI, labels: labelPi }}
                            />
                            <Plot.OfX y={(x) => Math.sin(x)} />
                        </Mafs>
                    )
                }
            """
        
        SubHeading "Making things interactive"
        let phase = Plot.useMovablePoint ([| 0.0; 0.0 |],
                                          constrain = !^Horizontal)
        Mafs ([
            prop.key 1
            prop.children [
                Cartesian (subdivisions = 4,
                           xAxis = { lines  = pi
                                     labels = Pi })
                Plot.OfX {| y = fun x -> sin (x - phase.x) |}
                phase.element
            ]
        ], viewBox = { ViewBox.Default with
                         x = vec2 -10 10
                         y = vec2 -2  2 }
         , preserveAspectRatio = !^false)
        CodeBlock
            """ import { Mafs, Coordinates, Plot, labelPi, useMovablePoint } from "mafs"

                function HelloFx() {
                    const phase = useMovablePoint([0, 0], {
                        constrain: "horizontal",
                    })

                    return (
                        <Mafs
                            viewBox={{ x: [-10, 10], y: [-2, 2] }}
                            preserveAspectRatio={false}
                        >
                        <Coordinates.Cartesian
                            subdivisions={4}
                            xAxis={{ lines: Math.PI, labels: labelPi }}
                        />
                        <Plot.OfX y={(x) => Math.sin(x - phase.x)} />
                        {phase.element}
                        </Mafs>
                    )
                }
            """
    ]
