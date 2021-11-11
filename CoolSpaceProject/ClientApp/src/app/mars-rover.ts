import { Camera } from "./camera";
import { Rover } from "./rover";

export interface MarsRover {
    id: number,
    sol: number,
    camera: Camera,
    img_src: string,
    earth_date: string,
    rover: Rover
}
