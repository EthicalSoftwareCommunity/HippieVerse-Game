extends TouchScreenButton

var radius = Vector2(28, 28)
var boundary = 64
var on_going_drag = -1
var return_accel = 20
var threshold = 2

func _process(delta):
	if on_going_drag == -1:
		var pos_difference = (Vector2(0, 0) - radius) - position
		position += pos_difference * return_accel * delta

func _input(event):
	if event is InputEventScreenDrag or (event is InputEventScreenTouch and event.is_pressed()):
		var event_dist_from_center = (event.position - get_parent().global_position).length()

		if event_dist_from_center <= boundary * global_scale.x or event.get_index() == on_going_drag:
			set_global_position(event.position - radius * global_scale)

			if get_button_pos().length() > boundary:
				set_position( get_button_pos().normalized() * boundary - radius)

			on_going_drag = event.get_index()

	if event is InputEventScreenTouch and !event.is_pressed() and event.get_index() == on_going_drag:
		on_going_drag = -1

func get_value():
	if get_button_pos().length() > threshold:
		return get_button_pos().normalized()
	return Vector2(0, 0)

func get_button_pos():
	return position + radius
