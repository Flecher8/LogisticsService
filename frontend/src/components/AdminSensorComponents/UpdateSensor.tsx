import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Form } from "react-bootstrap";
import { UpdateModelProps } from "../../helpers/interfaces/UpdateModelProps";
import { SensorDto, SensorsService } from "../../api/services/SensorsService";

export const UpdateSensor: FC<UpdateModelProps<SensorDto>> = (props: UpdateModelProps<SensorDto>) => {
	const [itemId, setItemId] = useState<number | undefined>(0);
	const [smartDeviceId, setSmartDeviceId] = useState<number>(0);

	async function handle() {
		try {
			let item: SensorDto = {
				sensorId: itemId,
				smartDeviceId: smartDeviceId
			};
			await SensorsService.prototype.update(item);
			props.close();
			window.location.reload();
		} catch (err) {
			alert(err);
		}
	}

	// onLoad function
	useEffect(() => {
		if (props.item !== undefined) {
			setItemId(props.item.sensorId);
			setSmartDeviceId(props.item.smartDeviceId);
		}
	}, []);

	return (
		<div className="UpdateSubscriptionType">
			<div className="container">
				<header>
					<div className="text-center mt-5">
						<h1>Update</h1>
					</div>
				</header>
				<div>
					<div className="mt-5 ml-5 mr-5">
						<Form>
							<Form.Group className="mb-3">
								<Form.Label>Smart device id</Form.Label>
								<InputGroup>
									<FormControl
										type="number"
										placeholder="Enter smart device id"
										value={smartDeviceId}
										onChange={e => setSmartDeviceId(parseInt(e.target.value))}
									/>
								</InputGroup>
							</Form.Group>
						</Form>
						<div>
							<Button className="mb-5" onClick={() => handle()}>
								Update
							</Button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
