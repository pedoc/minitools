use std::path::Path;

pub mod common {
    pub fn path_exists(path: &str) -> bool {
        Path::new(path).exists()
    }
}