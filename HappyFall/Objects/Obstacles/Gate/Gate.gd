extends Tunnel

func is_type(type): return type == "Gate" or .is_type(type)
func	get_type(): return "Gate"

class_name Gate

func _ready():
	tunnel = $tunnel_common
	obstacle = $Gate
