module Mafs.Examples

open System

open Fable.Core.JsInterop
open Feliz

open Feliz.Mafs

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
    Html.code [
        prop.className "text-slate-200 bg-black"
        prop.text text
    ]

[<ReactComponent>]
let GetStarted () =
    Html.div [
        Heading "Get Started Examples"

        SubHeading "Drawing a coordinate plane"
        MafsDefault [
            Cartesian CartesianProps.Default
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

        MafsDefault [
            Cartesian { CartesianProps.Default with subDiv = 4 }
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
        MafsDefault [
            Cartesian { CartesianProps.Default with subDiv = 4 }
            Plot.OfX <| fun x -> sin x
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

        Mafs { MafsProps.Default with
                 viewBox = { ViewBox.Default with
                               x = [| -10; 10 |]
                               y = [| -2 ;  2 |] }
                 preserveAspectRatio = !^false }
            [
                Cartesian { xAxis  = { lines = Math.PI; labels = Labels.Pi }
                            yAxis  = Axis.Default
                            subDiv = 4 }
                Plot.OfX <| fun x -> sin x
            ]
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
        let phase = useConstrainedPoint [| 0.0; 0.0 |] !^Horizontal
        Mafs { MafsProps.Default with
                 viewBox = { ViewBox.Default with
                               x = [| -10; 10 |]
                               y = [| -2 ; 2  |] }
                 preserveAspectRatio = !^false }
            [
                Cartesian { xAxis  = { lines = Math.PI; labels = Labels.Pi }
                            yAxis  = Axis.Default
                            subDiv = 4 }
                Plot.OfX <| fun x -> sin (x - phase.x)
                phase.element
            ]
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

[<ReactComponent>]
let Text () =
    Html.div [
        Heading "Text"
        MafsDefault [
            Text "I love maths!" [| 0.0; 0.0 |]
        ]
        CodeBlock
            """ import { Mafs, Text } from "mafs"

                function Example() {
                    return (
                        <Mafs>
                            <Text x={0} y={0}>I love math!</Text>
                        </Mafs>
                    )
                }
            """
    ]

[<ReactComponent>]
let Camera () =
    Html.div [
        Heading "Camera Controls"
        Mafs { MafsProps.Default with
                 zoom = !^{ min = 0.1
                            max = 2.0 }
                 viewBox = { x = [| -0.25; 0.25 |]
                             y = [| -0.25; 0.25 |]
                             padding = 0 } }
            [
                Cartesian { CartesianProps.Default with subDiv = 4 }
                Circle [| 0; 0 |] 1
                TextAttach "Oh hi!" [| 1.1; 0.1 |] NorthEast
            ]
        CodeBlock
            """ import { Mafs, Coordinates, Circle, Text } from "mafs"

                function ZoomExample() {
                    return (
                        <Mafs
                            zoom={{ min: 0.1, max: 2 }}
                            viewBox={{
                                x: [-0.25, 0.25],
                                y: [-0.25, 0.25],
                                padding: 0,
                            }}
                        >
                            <Coordinates.Cartesian subdivisions={5} />
                            <Circle center={[0, 0]} radius={1} />
                            <Text x={1.1} y={0.1} attach="ne">
                                Oh hi!
                            </Text>
                        </Mafs>
                    )
                }
            """

        Mafs { MafsProps.Default with
                 viewBox = { ViewBox.Default with
                               x = [| -5; 5 |]
                               y = [| -5; 5 |] } }
            [
                Cartesian CartesianProps.Default
                Polygon [|[| -5; -5 |]; [| 5; -5 |]; [| 5; 5 |]; [| -5; 5 |]|]
            ]
        CodeBlock
            """ import { Mafs, Coordinates, Polygon } from "mafs"

                function ViewboxEample() {
                    return (
                        <Mafs viewBox={{ x: [-5, 5], y: [-5, 5] }}>
                            <Coordinates.Cartesian />
                            <Polygon points={[[-5, -5], [5, -5], [5, 5], [-5, 5]]} />
                        </Mafs>
                    )
                }
            """
    ]