import { useState, useEffect, FC } from "react";
import { Link } from "react-router-dom";
import { Button, InputGroup, FormControl, Table, Modal, Form } from "react-bootstrap";
import { SubscriptionType, SubscriptionTypesService } from "../../api/services/SubscriptionTypesService";
import { CreateModelProps } from "../../helpers/interfaces/ChangeModelProps";

export const CreateSubscriptionType: FC<CreateModelProps> = (props: CreateModelProps) => {
	const [subscriptionTypeName, setSubscriptionTypeName] = useState<string>("");
	const [durationInDays, setDurationInDays] = useState<number>(0);
	const [price, setPrice] = useState<number>(0);

	async function handle() {
		try {
			let subscriptionType: SubscriptionType = {
				subscriptionTypeName: subscriptionTypeName,
				durationInDays: durationInDays,
				price: price
			};
			await SubscriptionTypesService.prototype.create(subscriptionType);
			props.close();
			window.location.reload();
		} catch (err) {
			alert(err);
		}
	}

	return (
		<div className="CreateSubscriptionType">
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
								<Form.Label>Subscription Type Name</Form.Label>
								<Form.Control
									type="text"
									placeholder="Enter subscription type name"
									value={subscriptionTypeName}
									onChange={e => setSubscriptionTypeName(e.target.value)}
								/>
							</Form.Group>

							<Form.Group className="mb-3">
								<Form.Label>Duration In Days</Form.Label>
								<Form.Control
									type="number"
									placeholder="Enter duration in days"
									value={durationInDays}
									onChange={e => setDurationInDays(parseInt(e.target.value))}
								/>
							</Form.Group>

							<Form.Group className="mb-3">
								<Form.Label>Price</Form.Label>
								<InputGroup>
									<FormControl
										type="number"
										placeholder="Enter price"
										value={price}
										onChange={e => setPrice(parseInt(e.target.value))}
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
