import { Container, Typography } from "@mui/material";
import { useState } from "react";
import agent from "../../app/api/agent";

export default function AboutPage() {
  const [validationErrors, setValidationErrors] = useState<string[]>([]);

  function getValidationError() {
    agent.TestErrors.getValidationError()
      .then(() => console.log("should not see this"))
      .catch((error) => setValidationErrors(error));
  }

  return (
    <Container>
      <Typography gutterBottom variant="h2">
        This is about
      </Typography>
    </Container>
  );
}
