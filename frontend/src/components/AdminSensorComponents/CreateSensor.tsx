import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Form } from "react-bootstrap";
import { CreateModelProps } from "../../helpers/interfaces/ChangeModelProps";
import { Sensor, SensorsService } from "../../api/services/SensorsService";

export const CreateSensor: FC<CreateModelProps> = (props: CreateModelProps) => {
	const [smartDeviceId, setSmartDeviceId] = useState<number>(0);

	async function handle() {
		try {
			let item: Sensor = {
				smartDeviceId: smartDeviceId
			};
			await SensorsService.prototype.create(item);
			props.close();
			window.location.reload();
		} catch (err) {
			// TODO Languages
			// Error: Specified argument was out of the range of valid values. (Parameter 'SmartDevice with such id does not exist')
			alert(err);
		}
	}

	return (
		<div className="CreateSensor">
			<div className="container">
				<header>
					<div className="text-center mt-5">
						<h1>Create</h1>
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
								Create
							</Button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
