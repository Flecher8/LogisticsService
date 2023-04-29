import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Form } from "react-bootstrap";
import { UpdateModelProps } from "../../helpers/interfaces/UpdateModelProps";
import { SmartDevice, SmartDevicesService } from "../../api/services/SmartDevicesService";

export const UpdateSmartDevice: FC<UpdateModelProps<SmartDevice>> = (props: UpdateModelProps<SmartDevice>) => {
	const [itemId, setItemId] = useState<number | undefined>(0);
	const [logisticCompanyId, setLogisticCompanyId] = useState<number>(0);
	const [numberOfSensors, setNumberOfSensors] = useState<number>(0);

	async function handle() {
		try {
			let item: SmartDevice = {
				smartDeviceId: itemId,
				logisticCompanyId: logisticCompanyId,
				numberOfSensors: numberOfSensors
			};
			await SmartDevicesService.prototype.update(item);
			props.close();
			window.location.reload();
		} catch (err) {
			alert(err);
		}
	}

	// onLoad function
	useEffect(() => {
		if (props.item !== undefined) {
			setItemId(props.item.smartDeviceId);
			setLogisticCompanyId(props.item.logisticCompanyId);
			setNumberOfSensors(props.item.numberOfSensors);
		}
	}, []);

	return (
		<div className="UpdateSmartDevice">
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
								<Form.Label>Logistic company id</Form.Label>
								<InputGroup>
									<FormControl
										type="number"
										placeholder="Enter logistic company id"
										value={logisticCompanyId}
										onChange={e => setLogisticCompanyId(parseInt(e.target.value))}
									/>
								</InputGroup>
							</Form.Group>
							<Form.Group className="mb-3">
								<Form.Label>Number of sensors</Form.Label>
								<InputGroup>
									<FormControl
										type="number"
										placeholder="Enter number of sensors"
										value={numberOfSensors}
										onChange={e => setNumberOfSensors(parseInt(e.target.value))}
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
