use std::env;
use std::io::{Result};

fn main() -> Result<()> {
    let _path = env::current_dir().unwrap();
    println!("{}", _path.display());
    Ok(())
}
