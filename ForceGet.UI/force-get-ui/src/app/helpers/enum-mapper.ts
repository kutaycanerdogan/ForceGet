import {
  Mode,
  MovementType,
  Incoterm,
  PackageType,
} from '../models/enums.model';

export const EnumMaps = {
  Mode: {
    0: Mode.LCL,
    1: Mode.FCL,
    2: Mode.Air,
  },
  MovementType: {
    0: MovementType.DoorToDoor,
    1: MovementType.PortToDoor,
    2: MovementType.DoorToPort,
    3: MovementType.PortToPort,
  },
  Incoterm: {
    0: Incoterm.DDP,
    1: Incoterm.DAT,
  },
  PackageType: {
    0: PackageType.Pallets,
    1: PackageType.Boxes,
    2: PackageType.Cartons,
  },
};
