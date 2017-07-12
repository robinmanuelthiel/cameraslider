# Camera Slider


## Controller
You can send the following commands to the controller via Bluetooth.

### Motor
- `on#` to turn the motor on
- `off#` to turn the motor off
- `dl#` to define *left* as the motor direction
- `dr#` to define *right* as the motor direction
- `spXXX#` to define the motor speed between 0 (fastest) to 999 (slowest)

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