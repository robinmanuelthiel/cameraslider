# Camera Slider
## Mobile App
**Android**

[![Build status](https://build.mobile.azure.com/v0.1/apps/05542a9b-1639-4e49-993e-0e3e2e060624/branches/master/badge)](https://mobile.azure.com)

## Controller
You can send the following commands to the controller via Bluetooth.

### Motor
- `on#` to turn the motor on
- `off#` to turn the motor off
- `dl#` to define *left* as the motor direction
- `dr#` to define *right* as the motor direction
- `spXXX#` to define the motor speed between 0 (fastest) to 999 (slowest)
- `shutter#` triggers the shutter

### Examples
To let the motor turn left with medium speed, send

```
dr#
sp300#
on#
```

To stop the motor send
```
stop#
```
