import { Group } from "@mui/icons-material";
import {
  AppBar,
  Box,
  Button,
  Container,
  MenuItem,
  MenuList,
  Toolbar,
  Typography,
} from "@mui/material";

export default function NavBar() {
  return (
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          background: "linear-gradient(90deg, #0F172A 0%, #1E293B 100%)",
          boxShadow: "0 4px 20px rgba(0,0,0,0.25)",
        }}
      >
        <Container maxWidth="xl">
          <Toolbar
            sx={{
              display: "flex",
              justifyContent: "space-between",
            }}
          >
            <MenuList>
              <MenuItem sx={{ display: "flex", gap: 2 }}>
                <Group sx={{ color: "#FBBF24" }} fontSize="large" />
                <Typography
                  variant="h4"
                  sx={{
                    fontWeight: "bold",
                    color: "#FFFFFF",
                  }}
                >
                  Events
                </Typography>
              </MenuItem>
            </MenuList>

            <MenuList
              disablePadding
              sx={{
                display: "flex",
                color: "#E2E8F0",
              }}
            >
              <MenuItem sx={{ fontSize: "1.2rem", textTransform: "uppercase" }}>
                Events
              </MenuItem>
              <MenuItem sx={{ fontSize: "1.2rem", textTransform: "uppercase" }}>
                About
              </MenuItem>
              <MenuItem sx={{ fontSize: "1.2rem", textTransform: "uppercase" }}>
                Contact
              </MenuItem>
            </MenuList>

            <Button
              size="large"
              variant="contained"
              sx={{
                backgroundColor: "#FBBF24",
                color: "#0F172A",
                fontWeight: "bold",
                "&:hover": {
                  backgroundColor: "#F59E0B",
                },
              }}
            >
              Create Event
            </Button>
          </Toolbar>
        </Container>
      </AppBar>
    </Box>
  );
}
