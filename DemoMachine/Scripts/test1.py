from Automation.FrameworkExtension.common import *
from Automation.FrameworkExtension.stateMachine import *
import sys


try:
	print(state)

	if state == RunningState.Resetting:
		t.Log(t.Name)
		t.Log("Run Script Resetting", LogLevel.Debug)
		pass
	elif state == RunningState.Running:
		t.Log(t.Name)
		t.Log("Run Script Running", LogLevel.Debug)
		pass
	else:
		print('state not equal to any')
		
except Exception as e:
	print(e)