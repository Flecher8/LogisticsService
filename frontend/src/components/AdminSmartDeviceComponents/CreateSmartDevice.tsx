import { useState, useEffect, FC } from "react";
import { Button, InputGroup, FormControl, Form } from "react-bootstrap";
import { CreateModelProps } from "../../helpers/interfaces/ChangeModelProps";
import { SmartDevice, SmartDevicesService } from "../../api/services/SmartDevicesService";

export const CreateSmartDevice: FC<CreateModelProps> = (props: CreateModelProps) => {
	const [logisticCompanyId, setLogisticCompanyId] = useState<number>(0);
	const [numberOfSensors, setNumberOfSensors] = useState<number>(0);

	async function handle() {
		try {
			let item: SmartDevice = {
				logisticCompanyId: logisticCompanyId,
				numberOfSensors: numberOfSensors
			};
			await SmartDevicesService.prototype.create(item);
			props.close();
			window.location.reload();
		} catch (err) {
			// TODO Languages
			alert(err);
		}
	}

	return (
		<div className="CreateSmartDevice">
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
								Create
							</Button>
						</div>
					</div>
				</div>
			</div>
		</div>
	);
};
